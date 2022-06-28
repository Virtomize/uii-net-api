namespace UII
{
    internal class ErrorResponse
    {
        public List<string> errors { get; set; } = new List<string>();
        public string timestamp { get; set;  } = string.Empty;
    }
}