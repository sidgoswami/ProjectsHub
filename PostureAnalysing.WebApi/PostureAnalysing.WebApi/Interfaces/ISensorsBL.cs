using PostureAnalysing.WebApi.Models;

namespace PostureAnalysing.WebApi.Interfaces
{
    public interface ISensorsBL
    {
        List<SensorsResponse> ValidateSensorValues(SensorsRequest sr);
    }
}
