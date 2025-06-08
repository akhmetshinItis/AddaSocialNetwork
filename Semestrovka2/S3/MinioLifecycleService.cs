using System.Text.Json;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Minio;
using Minio.DataModel.Args;

namespace S3
{
    public class MinioLifecycleService : IHostedService
    {
        private readonly IMinioClient _minioClient;
        private readonly S3Options _s3Options;

        public MinioLifecycleService(IMinioClient minioClient, IOptions<S3Options> s3Options)
        {
            _minioClient = minioClient;
            _s3Options = s3Options.Value;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var bucketName = _s3Options.BucketName;

            bool exists = await _minioClient.BucketExistsAsync(
                new BucketExistsArgs().WithBucket(bucketName), cancellationToken);

            if (!exists)
            {
                await _minioClient.MakeBucketAsync(
                    new MakeBucketArgs().WithBucket(bucketName), cancellationToken);

                Console.WriteLine($"Bucket '{bucketName}' created successfully.");
            }
            else
            {
                Console.WriteLine($"Bucket '{bucketName}' already exists.");
            }
            
            // Преобразуем объект политики в строку JSON
            string policyJson = JsonSerializer.Serialize(_s3Options.PublicReadPolicy);

            await _minioClient.SetPolicyAsync(new SetPolicyArgs()
                .WithBucket(bucketName)
                .WithPolicy(policyJson), cancellationToken);

            Console.WriteLine($"Public read policy applied to bucket '{bucketName}'.");
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}