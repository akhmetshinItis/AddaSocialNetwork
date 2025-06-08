using Core.Abstractions;
using Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Minio;
using Minio.DataModel.Args;

namespace S3
{
    public class S3Service : IS3Service
    {
        /// <summary>
        /// Тип данных файла по умолчанию
        /// </summary>
        private const string DefaultContentType = "application/octet-stream";
        
        private readonly IMinioClient _client;
        private readonly IOptions<S3Options> _s3Options;
        

        public S3Service(IMinioClient client, IOptions<S3Options> s3Options)
        {
            _client = client;
            _s3Options = s3Options;
        }

        public async Task<string> UploadAsync(IFormFile file, CancellationToken cancellationToken = default)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("Файл не предоставлен или пустой.", nameof(file));

            var bucketName = _s3Options.Value.BucketName;

            // Уникальное имя файла
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            var contentType = string.IsNullOrWhiteSpace(file.ContentType) ? "application/octet-stream" : file.ContentType;

            // Загружаем файл
            using var stream = file.OpenReadStream();

            var putObjectArgs = new PutObjectArgs()
                .WithBucket(bucketName)
                .WithObject(fileName)
                .WithStreamData(stream)
                .WithObjectSize(stream.Length)
                .WithContentType(contentType);

            await _client.PutObjectAsync(putObjectArgs, cancellationToken);

            // Формируем публичную ссылку (если хранилище публичное)
            var endpoint = _s3Options.Value.ServiceUrl.TrimEnd('/');
            var url = $"http://{endpoint}/{bucketName}/{fileName}";

            return url;
        }
    }
}