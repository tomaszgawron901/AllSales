using AllSales.Shared.Models;

namespace AllSales.Shared.Services;

public interface IOutputService
{
    Task ClearProducts();
    Task AddProducts(List<Product> products);
}
