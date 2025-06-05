using Core.Models;

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
        Task UploadAsync(
            FileContent file,
            CancellationToken cancellationToken = default);
    }
}