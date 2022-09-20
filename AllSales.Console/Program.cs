using AllSales.Console;
using AllSales.Shared.Services;
using HmInput;
using NotionOutput;

IOutputService outputService = new NotionOutputService();
IInputService[] inputServices = new IInputService[]
{
    new HmInputService(new HttpClient()),
};

var app = new Application(outputService, inputServices);
await app.Run();