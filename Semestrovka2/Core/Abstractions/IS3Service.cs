using Microsoft.AspNetCore.Http;

namespace Core.Abstractions
{
    public interface IS3Service
    {
        /// <summary>
        /// Загрузить файл в хранилище
        /// </summary>
        /// <param name="file">Файл для сохранения в S3</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>ИД файла в хранилище</returns>
        Task<String> UploadAsync(
            IFormFile file,
            CancellationToken cancellationToken = default);
    }
}