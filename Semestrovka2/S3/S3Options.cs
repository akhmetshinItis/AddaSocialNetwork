namespace S3
{
    public class S3Options
    {
        /// <summary>
        /// Логин
        /// </summary>
        public string AccessKey { get; set; } = default!;

        /// <summary>
        /// Секрет
        /// </summary>
        public string SecretKey { get; set; } = default!;

        /// <summary>
        /// УРЛ хранилища
        /// </summary>
        public string ServiceUrl { get; set; } = default!;

        /// <summary>
        /// Название бакета
        /// </summary>
        public string BucketName { get; set; } = default!;
        
        public string Prefix { get; set; } = "https";
    }
}