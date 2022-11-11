using AllSales.Console;
using AllSales.Shared.Services;
using HmInput;
using Microsoft.Extensions.Configuration;
using NotionOutput;




if (Environment.GetEnvironmentVariable("Notion:AuthToken") is null)
{
    var builder = new ConfigurationBuilder();
    builder.AddUserSecrets<Program>();
    IConfiguration Configuration = builder.Build();
    Environment.SetEnvironmentVariable("Notion:AuthToken", Configuration.GetSection("Notion:AuthToken").Value);
}

IOutputService outputService = new NotionOutputService();
IInputService[] inputServices = new IInputService[]
{
    new HmInputService(new HttpClient()),
};

var app = new Application(outputService, inputServices);
await app.Run();