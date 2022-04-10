namespace PostureAnalysing.WebApi.Models
{
    public class AppSettings
    {
        public SensorRange? Cervical { get; set; }
        public SensorRange? Thoracic { get; set; }
        public SensorRange? Lumbar { get; set; }

    }
}
