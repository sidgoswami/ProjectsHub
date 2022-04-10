namespace PostureAnalysing.WebApi.Models
{
    public class DetailedReport
    {
        public DateTime StartTimeStamp { get; set; }
        public DateTime FinishTimeStamp { get; set; }

        public RequestValidationResult IsValid()
        {
            var requestValidation = new RequestValidationResult(); ;
            if (StartTimeStamp == null)
            {
                requestValidation.Errors.Add("StartTimeStamp cannot be null.");
            }
            if (FinishTimeStamp == null)
            {
                requestValidation.Errors.Add("FinishTimeStamp cannot be null.");
            }
            if ((FinishTimeStamp - StartTimeStamp).TotalDays >= Constants.AllowedReportDays)
            {
                requestValidation.Errors.Add($"Report can be generated for a maximum of {Constants.AllowedReportDays} days.");
            }
            return requestValidation;
        }
    }
}
