# MobileCenter.NetSdk
This is a mapping of the Mobile Center API for .NET Standard 1.4 written in C#. At the moment only calls for Account & Build are implemented (for reference please see https://docs.mobile.azure.com/api/). Feel free to add more calls and create a pull request. There are also unit tests for each call, this might help to understand the features while the documentation is incomplete.

A short sample to get started:

```csharp
var client = new MobileCenterSdkClient("{your-api-key}");
//get all apps associated with the account
var apps = await client.AccountService.GetAppsAsync();
//invite user to the first app in the list
await apps.First().InviteUserAsync("xyz@useremail.com");

```

License: [MIT License](LICENSE)