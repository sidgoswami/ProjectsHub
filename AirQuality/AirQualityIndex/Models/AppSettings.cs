namespace AirQualityIndex.Models
{
    public class AppSettings
    {
        public string ApiUrl { get; init; }
        public string ApiKey { get; init; }
        public string ConnectionString { get; init; }
        public string StorageTableName { get; set; }
    }
}
