using UII;

var client = new UII.Client("Token here");

var osList = await client.ReadOsList();
Console.WriteLine("Supported operations systems:");
Console.WriteLine(string.Join('\n',osList.Select(os => os.DisplayName)));

var packageList = await client.ReadPackageList("debian", "10", "x86_64");
Console.WriteLine($"Found {packageList.Packages.Count()} packages for {packageList.Distribution} version {packageList.Version}");

await client.Build(
    "c:/tmp/debian.iso",
    "debian",
    "10",
    "x86_64",
    "hostName" ,
    new List<Network>
    {
        new ()
        {
            HasNoInternet = false,
            UsesDhcp = true,
        },
    });
