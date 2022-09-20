using AllSales.Shared.Services;

namespace AllSales.Console;

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
        await outputService.ClearProducts();
        foreach (var service in inputServices)
        {
            var products = await service.GetSaleProducts();
            await outputService.AddProducts(products);
        }
    }
}
