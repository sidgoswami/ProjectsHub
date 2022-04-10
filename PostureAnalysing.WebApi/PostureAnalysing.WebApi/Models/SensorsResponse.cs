namespace PostureAnalysing.WebApi.Models
{
    public class SensorsResponse
    {
        public string SensorName { get; set; }
        public string Message { get; set; }
        public bool IsCorrect { get; set; }
        public SensorsResponse(string sensorName) => SensorName = sensorName;
    }
}
