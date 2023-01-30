namespace Extremis.Client.Services.HttpClients;

public class PublicHttpClient
{
    public HttpClient Client { get; }

    public PublicHttpClient(HttpClient client)
    {
        Client = client;
    }
}