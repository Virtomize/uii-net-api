namespace UII
{
    internal class BuildRequest
    {
        public string arch { get; init; } = string.Empty;
        public string dist { get; init; } = string.Empty;
        public string version { get; init; } = string.Empty;
        public string hostname { get; init; } = string.Empty;
        public IEnumerable<BuildRequestNetwork> networks { get; init; } = Array.Empty<BuildRequestNetwork>();
    }
}