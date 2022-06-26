class Program
{
    static async Task Main(string[] args)
    {
        var client = new Client("");
        await client.Build(
            "debian",
                "10",
            "x86_64",
            "horst" ,
            new List<Network>
                    {
                        new ()
                        {
                            HasNoInternet = false,
                            UsesDhcp = true,
                            //IpNetmask = "192.168.0.200/16"
                        },
                    });
    }
}
