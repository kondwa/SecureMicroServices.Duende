namespace ID.Serve.Models
{
    public class ExceptionDto
    {
        public string Id { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string StackTrace { get; set; }
        public string Source { get; set; }
    }
}
