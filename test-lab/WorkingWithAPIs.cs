using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace test_lab;

internal class WorkingWithAPIs
{
    public static async Task Exe()
    {
        //para hacer peticiones HTTP
        using HttpClient client = new();

        //setting up client , todo lo necesario Headers, User-agent, etc
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
        client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

        //recupera todos los repo de cierta institucion empleando la API de github
        var repositories = await ProcessRepositoryAsync(client);

        //imprime el contenido
        foreach (var repo in repositories)
        {
            Console.WriteLine($"Name: {repo.Name}");
            Console.WriteLine($"Homepage: {repo.Homepage}");
            Console.WriteLine($"GitHub: {repo.GitHubHomeUrl}");
            Console.WriteLine($"Description: {repo.Description}");
            Console.WriteLine($"Watchers: {repo.Watchers:#,0}");
            Console.WriteLine($"Last push: {repo.LastPush}");
            Console.WriteLine();
        }
    }
    static async Task<List<Repository>> ProcessRepositoryAsync(HttpClient client)
    {
        //using getSTRINGasync
        //var json = await client.GetStringAsync(
        //     "https://api.github.com/orgs/dotnet/repos");


        //HERE U HAVE TO USE GetStreamAsync METHOD
        await using Stream stream = await client.GetStreamAsync("https://api.github.com/orgs/dotnet/repos");

        var repositories = await JsonSerializer.DeserializeAsync<List<Repository>>(stream);

        return repositories;
    }

    private record class Repository(
        [property: JsonPropertyName("name")] string Name,
        [property: JsonPropertyName("description")] string Description,
        [property: JsonPropertyName("html_url")] Uri GitHubHomeUrl,
        [property: JsonPropertyName("homepage")] Uri Homepage,
        [property: JsonPropertyName("watchers")] int Watchers,
        [property: JsonPropertyName("pushed_at")] DateTime LastPushUtc)
    {
        public DateTime LastPush => LastPushUtc.ToLocalTime();
    }


    //segun yo debiera funcionar esto tmb
    //private record class Repository
    //{
    //    [JsonPropertyName("name")] public string Name;
    //    [JsonPropertyName("description")] public string Description;
    //    [JsonPropertyName("html_url")] public Uri GitHubHomeUr;
    //    [JsonPropertyName("homepage")] public Uri Homepage;
    //    [JsonPropertyName("watchers")] public int Watchers;
    //    [JsonPropertyName("pushed_at")] public DateTime LastPushUtc;

    //    public DateTime LastPush => LastPushUtc.ToLocalTime();
    //}


}
