namespace UII;

internal class BuildRequestNetwork
{
    public static BuildRequestNetwork FromNetwork(Network n)
    {
        return new BuildRequestNetwork()
        {
            dhcp = n.UsesDhcp,
            domain = n.Domain,
            dns = n.DNSs,
            gateway = n.Gateway,
            ipnet = n.IpNetmask,
            nointernet = n.HasNoInternet
        };
    }

    public bool dhcp { get; init; }

    public string domain { get; init; } = string.Empty;
    
    public IEnumerable<string> dns{ get; init; } = Array.Empty<string>();

    public string ipnet { get; init; } = string.Empty;

    public string gateway { get; init; } = string.Empty;

    public bool nointernet { get; init; }
}