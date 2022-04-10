namespace PostureAnalysing.WebApi.Models
{
    public class RequestValidationResult
    {
        public List<string> Errors { get; set; }
        private string Separator { get; init; }

        public bool IsValid
        {
            get 
            {
                if (Errors.Count > 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
        public RequestValidationResult(string separator = "|")
        {
            Separator = separator;
            Errors = new List<string>();
        }

        public string ErrorAsString { get {
                return string.Join(Separator, Errors);
            } 
        }
    }
}
