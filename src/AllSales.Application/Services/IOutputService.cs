using AllSales.Shared.Models;

namespace AllSales.Application.Services;

public interface IOutputService
{
    Task Pull();
    Task Push();
    void AddOrUpdate(Product product);
}
