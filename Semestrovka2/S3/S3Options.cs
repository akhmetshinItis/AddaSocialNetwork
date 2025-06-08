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
        
        public PublicReadPolicy PublicReadPolicy { get; set; } = null!;
    }
    
    public class PublicReadPolicy
    {
        public string Version { get; set; } = null!;
        public List<Statement> Statement { get; set; } = new();
    }

    public class Statement
    {
        public string Effect { get; set; } = null!;
        public Dictionary<string, string> Principal { get; set; } = new();
        public List<string> Action { get; set; } = new();
        public List<string> Resource { get; set; } = new();
    }
}