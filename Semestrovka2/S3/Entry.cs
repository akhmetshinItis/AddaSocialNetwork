using Core.Abstractions;
using Microsoft.Extensions.DependencyInjection;
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

            services.AddSingleton(options);
            services.AddSingleton<IS3Service, S3Service>();
            services.AddSingleton<IMinioClient, MinioClient>();
            return services;
        }
    }
}