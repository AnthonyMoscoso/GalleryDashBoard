namespace Bsn.RestServices.Models
{
    public class ErrorResult
    {
        public int StatusCode { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
