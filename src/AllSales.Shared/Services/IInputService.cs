using AllSales.Shared.Models;

namespace AllSales.Shared.Services;

public interface IInputService
{
    Task<List<Product>> GetSaleProducts();
}
