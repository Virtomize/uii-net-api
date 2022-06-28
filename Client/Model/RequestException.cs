namespace UII
{
    public class RequestException : Exception
    {
        public RequestException(List<string> errors): base($"was not successful and returned errors {string.Join(';', errors)}")
        {}
    }
}