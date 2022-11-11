using AllSales.Shared.Models;

namespace AllSales.Shared.Services;

public interface IOutputService
{
    Task Pull();
    Task Push();
    void AddOrUpdate(Product product);
}
