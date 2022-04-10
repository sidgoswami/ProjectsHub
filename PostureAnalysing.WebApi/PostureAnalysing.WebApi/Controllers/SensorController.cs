using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PostureAnalysing.WebApi.Interfaces;
using PostureAnalysing.WebApi.Models;

namespace PostureAnalysing.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SensorController : ControllerBase
    {
        private readonly ISensorsBL sensorsBL;

        public SensorController(ISensorsBL sensors)
        {
            sensorsBL = sensors;
        }

        /// <summary>
        /// Send the report of all the sensors value to the api
        /// </summary>
        /// <param name="sr"></param>
        /// <returns></returns>
        [HttpPost("report")]
        public GenericResponse<string> Report(SensorsRequest sr)
        {
            var validation = sr.IsValid();
            if (validation.Errors.Count > 0)
            {
                var genericResponse = new GenericResponse<string>(false, validation.ErrorAsString, "");
                return genericResponse;
            }
            else
            {
                var sensorChecks = sensorsBL.ValidateSensorValues(sr);
                if (sensorChecks.Any(sc => !sc.IsCorrect))
                {
                    return new GenericResponse<string>(false, "Some sensors found incorrect angle", JsonConvert.SerializeObject(sensorChecks));
                }
                else
                {
                    return new GenericResponse<string>(true, "Posture is Correct", JsonConvert.SerializeObject(sensorChecks));
                }
            }
        }
    }
}
