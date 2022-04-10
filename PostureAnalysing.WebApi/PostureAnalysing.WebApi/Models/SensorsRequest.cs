using System.Text;

namespace PostureAnalysing.WebApi.Models
{
    public class SensorsRequest
    {
        public string User { get; set; }
        public float Cervical { get; set; }
        public float Thoracic { get; set; }
        public float Lumbar { get; set; }

        public RequestValidationResult IsValid()
        {
            var requestValidationResult = new RequestValidationResult();

            if (User == null)
            {
                requestValidationResult.Errors.Add("User cannot be null.");
            }
            if (Cervical is >= -180 and <= 180)
            {
                requestValidationResult.Errors.Add("Cervical sensor should be between -180 and 180.");
            }
            if (Thoracic is >= -180 and <= 180)
            {
                requestValidationResult.Errors.Add("Thoracic sensor should be between -180 and 180.");
            }
            if (Lumbar is >= -180 and <= 180)
            {
                requestValidationResult.Errors.Add("Lumbar sensor should be between -180 and 180.");
            }

            return requestValidationResult;
        }
    }
}
