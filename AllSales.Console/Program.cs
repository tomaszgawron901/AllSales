using Notion.Client;

DatabasesClient CreateDatabasesClient()
{
    var clientOptions = new ClientOptions
    {
        AuthToken = "secret_2tjAeD4JT3h1IJA6S8EimxD9gRNfULteXqdCbTHgVFp",
    };
    var restClient = new RestClient(clientOptions);
    var databasesClient = new DatabasesClient(restClient);
    return databasesClient;
}


var databasesClient = CreateDatabasesClient();
var response = await databasesClient.QueryAsync("54ba9dc0ebe94b109ec8608497515420", new DatabasesQueryParameters());
Console.WriteLine();

