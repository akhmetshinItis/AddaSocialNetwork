using Core.Abstractions;
using Core.Models;
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
        private readonly S3Options _s3Options;
        

        public S3Service(IMinioClient client, S3Options s3Options)
        {
            _client = client;
            _s3Options = s3Options;
        }

        public async Task UploadAsync(FileContent file, CancellationToken cancellationToken = default)
        {
            if (file?.Content == null)
                throw new ArgumentNullException(nameof(file));
            if (file?.FileName == null)
                throw new ArgumentException(nameof(file.FileName));

            var contentType = string.IsNullOrWhiteSpace(file.ContentType) ? DefaultContentType : file.ContentType;

            var put = new PutObjectArgs()
                .WithBucket(_s3Options.BucketName)
                .WithStreamData(file.Content)
                .WithContentType(contentType)
                .WithFileName(file.FileName);

            await _client.PutObjectAsync(put, cancellationToken);
        }
    }
}