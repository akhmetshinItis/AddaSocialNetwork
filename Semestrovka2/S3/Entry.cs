using Core.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Minio;

namespace S3
{
    public static class Entry
    {
        public static IServiceCollection AddS3Storage(this IServiceCollection services, Action<S3Options> options)
        {
            var storageOptions = new S3Options();
            options?.Invoke(storageOptions);

            return AddS3Storage(services, storageOptions);
        }
        
        public static IServiceCollection AddS3Storage(this IServiceCollection services, S3Options options)
        {
            ArgumentNullException.ThrowIfNull(options);
            if (string.IsNullOrWhiteSpace(options.AccessKey))
                throw new ArgumentException(nameof(options.AccessKey));
            if (string.IsNullOrWhiteSpace(options.BucketName))
                throw new ArgumentException(nameof(options.BucketName));
            if (string.IsNullOrWhiteSpace(options.SecretKey))
                throw new ArgumentException(nameof(options.SecretKey));
            if (string.IsNullOrWhiteSpace(options.ServiceUrl))
                throw new ArgumentException(nameof(options.ServiceUrl));

            services.Configure<S3Options>(_ =>
            {
                _.AccessKey = options.AccessKey;
                _.SecretKey = options.SecretKey;
                _.ServiceUrl = options.ServiceUrl;
                _.BucketName = options.BucketName;
            });
            
            services.AddSingleton<IS3Service, S3Service>();
            services.AddSingleton<IMinioClient>(provider =>
            {
                var s3Options = provider.GetRequiredService<IOptions<S3Options>>().Value;

                var minioClient = new MinioClient()
                    .WithEndpoint(s3Options.ServiceUrl)
                    .WithCredentials(s3Options.AccessKey, s3Options.SecretKey)
                    .Build();

                return minioClient;
            });

            services.AddHostedService<MinioLifecycleService>();
            return services;
        }
    }
}