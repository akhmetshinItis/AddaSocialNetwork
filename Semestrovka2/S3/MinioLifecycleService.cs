using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Minio;
using Minio.DataModel.Args;

namespace S3
{
    public class MinioLifecycleService : IHostedService
    {
        private readonly IMinioClient _minioClient;
        private readonly string _bucketName;

        public MinioLifecycleService(IMinioClient minioClient, IOptions<S3Options> options)
        {
            _minioClient = minioClient;
            _bucketName = options.Value.BucketName ?? throw new InvalidOperationException("BucketName is not set");
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            bool exists = await _minioClient.BucketExistsAsync(new BucketExistsArgs().WithBucket(_bucketName), cancellationToken);

            if (!exists)
            {
                await _minioClient.MakeBucketAsync(new MakeBucketArgs().WithBucket(_bucketName), cancellationToken);
                Console.WriteLine($"Bucket '{_bucketName}' created successfully.");
            }
            else
            {
                Console.WriteLine($"Bucket '{_bucketName}' already exists.");
            }
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}