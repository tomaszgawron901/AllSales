using AllSales.Application.Services;

namespace AllSales.Application;

public class Application
{
    private readonly IOutputService outputService;
    private readonly IInputService[] inputServices;

    public Application(IOutputService outputService, params IInputService[] inputServices)
    {
        this.outputService = outputService;
        this.inputServices = inputServices;
    }

    public async Task Run()
    {
        await outputService.Pull();
        foreach (var service in inputServices)
        {
            var products = await service.GetSaleProducts();
            foreach (var product in products)
            {
                outputService.AddOrUpdate(product);
            }
        }
        await outputService.Push();
    }
}
