
using Duende.IdentityModel.Client;
using Newtonsoft.Json;
using System.Net.Http.Json;

Console.WriteLine("Press any key to start");
Console.ReadLine();
var client = new HttpClient();
var disco = await client.GetDiscoveryDocumentAsync("https://localhost:7000");
var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
{
    Address = disco.TokenEndpoint,
    ClientId = "client.console",
    ClientSecret = "console-secret",
    Scope = "service.one.read service.two.write"
});
if (tokenResponse.IsError)
{
    Console.WriteLine(tokenResponse.Error);
    return;
}
var service1Client = new HttpClient();
service1Client.SetBearerToken(tokenResponse.AccessToken ?? string.Empty);
var response1 = await service1Client.GetAsync("https://localhost:7001/api/one/read");
Console.WriteLine(await response1.Content.ReadAsStringAsync());
var service2Client = new HttpClient();
service2Client.SetBearerToken(tokenResponse.AccessToken ?? string.Empty);
string data = "Hello from Console Client";
var json = JsonConvert.SerializeObject(data);
var response2 = await service1Client.PostAsJsonAsync("https://localhost:7002/api/two/write",json);
Console.WriteLine(await response2.Content.ReadAsStringAsync());
Console.WriteLine("Press any key to exit");
Console.ReadLine();