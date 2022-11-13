using AllSales.Shared.Models;

namespace AllSales.Application.Services;

public interface IInputService
{
    Task<List<Product>> GetSaleProducts();
}
