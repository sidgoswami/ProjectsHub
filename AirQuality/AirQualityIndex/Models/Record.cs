namespace AirQualityIndex.Models
{
    public class Record
    {
        public string id { get; set; }
        public string country { get; set; }
        public string state { get; set; }
        public string city { get; set; }
        public string station { get; set; }
        public string last_update { get; set; }
        public string pollutant_id { get; set; }
        public string pollutant_min { get; set; }
        public string pollutant_max { get; set; }
        public string pollutant_avg { get; set; }
        public string pollutant_unit { get; set; }
    }
}
