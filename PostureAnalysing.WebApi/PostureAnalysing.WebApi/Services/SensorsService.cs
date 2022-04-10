using Microsoft.Extensions.Options;
using PostureAnalysing.WebApi.Interfaces;
using PostureAnalysing.WebApi.Models;

namespace PostureAnalysing.WebApi.Services
{
    public class SensorsService : ISensorsBL
    {
        public AppSettings SensorSettings { get; set; }
        public SensorsService(IOptions<AppSettings> appSettings) => SensorSettings = appSettings.Value;

        /// <summary>
        /// Validates the individual sensor values to be in the right range
        /// </summary>
        /// <param name="sr"></param>
        /// <returns></returns>
        public List<SensorsResponse> ValidateSensorValues(SensorsRequest sr)
        {
            List<SensorsResponse> allSensorsValidation = new();
            var allSensors = Enum.GetValues<Sensor>();
            foreach (var sensor in allSensors)
            {
                SensorsResponse res = new(sensor.ToString());
                var currentSensorLimits = typeof(AppSettings).GetProperty(sensor.ToString()).GetValue(SensorSettings);
                var sensorValue = (float) sr.GetType().GetProperty(sensor.ToString()).GetValue(sr);
                float lowerLimit = (float) currentSensorLimits.GetType().GetProperty(Constants.LowBound).GetValue(currentSensorLimits);
                float upperLimit = (float) currentSensorLimits.GetType().GetProperty(Constants.HighBound).GetValue(currentSensorLimits);
                if (sensorValue >= lowerLimit && sensorValue <= upperLimit)
                {
                    res.IsCorrect = true;
                    res.Message = "Correct Posture";
                }
                else if (sensorValue >= upperLimit || sensorValue <= lowerLimit)
                {                    
                    res.IsCorrect = false;
                    res.Message = "Incorrect Posture";
                }
                allSensorsValidation.Add(res);
            }
            return allSensorsValidation;
        }
    }
}
