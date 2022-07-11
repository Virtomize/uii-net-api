[![Donate](https://img.shields.io/badge/Donate-PayPal-green.svg)](https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=VBXHBYFU44T5W&source=url)
[![License](https://img.shields.io/badge/license-MIT-blue.svg)](https://github.com/virtomize/uii-go-api/blob/master/LICENSE)
[![Website](https://img.shields.io/badge/%40-Virtomize-%231e828c)](https://virtomize.com)
[![Twitter](https://badgen.net/badge/icon/twitter?icon=twitter&label)](https://twitter.com/virtomize)
[![LinkedIn](https://img.shields.io/badge/linkedIn-%20-blue.svg?style=social&logo=linkedin)](https://www.linkedin.com/company/virtomize/)

# UUI .Net API
This repository contains an implementation of a client for the [**Virtomize Unattended Install Images API**](https://uii.virtomize.com/).

## Add to project

To use this client, first add it's nuget package


## Build an ISO
Building an ISO requires two steps.
1. Create a client object by using the `NewClient` function.
   This requires the API token created in the UI (see below).

```csharp
    var client = new UII.Client("Token here");
```

2. Building the ISO by using `Build` on the client object.
    ```csharp
   await client.Build(
        "c:/tmp/debian.iso",
        "debian",
        "10",
        "x86_64",
        "hostname" ,
        new List<Network>
        {   
            new ()
            {
             HasNoInternet = false,
             UsesDhcp = true,
            },
        });
   ```

   `Build` requires the following parameters:
   - A path to the output file
   - The name of the distribution
   - The version number of the distribution
   - The architecture of the distribution (`x86_64` in most cases)
   - A configuration of the network interfaces

## Register an API token
Before using this client library, you need to register an API token under your account.
For this, login into the [website](virtomize.com) and navigate to the "API-Token" tab.

![API-Token tab](https://github.com/Virtomize/uii_go_api/blob/60f79a50fc429f630eba553aaf057e6daa12ef97/doc/api-token.png "API-Token tab")

There, create a new API token by clicking "Create" and give it a name. Then click save.
![Create a token](https://github.com/Virtomize/uii_go_api/blob/60f79a50fc429f630eba553aaf057e6daa12ef97/doc/api-token-create.png "Create a token")

A new token will be created.
Copy this token, as it will be hidden, once you log out.
There, create a new API token by clicking "Create" and give it a name. Then click save.
![Save token](https://github.com/Virtomize/uii_go_api/blob/60f79a50fc429f630eba553aaf057e6daa12ef97/doc/api-token-created.png "Save token")

