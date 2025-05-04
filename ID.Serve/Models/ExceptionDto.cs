namespace ID.Serve.Models
{
    public class ExceptionDto
    {
        public string? Id { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; } = string.Empty;
        public string StackTrace { get; set; } = string.Empty;
        public string Source { get; set; } = string.Empty;
    }
}
