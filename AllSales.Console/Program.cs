using AllSales.Console;
using AllSales.Shared.Services;
using HmInput;
using Microsoft.Extensions.Configuration;
using NotionOutput;


var builder = new ConfigurationBuilder();
builder.AddUserSecrets<Program>();
IConfiguration Configuration = builder.Build();

IOutputService outputService = new NotionOutputService(Configuration);
IInputService[] inputServices = new IInputService[]
{
    new HmInputService(new HttpClient()),
};

var app = new Application(outputService, inputServices);
await app.Run();