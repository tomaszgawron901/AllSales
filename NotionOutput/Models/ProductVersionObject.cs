using AllSales.Shared.Models;
using NotionOutput.Enums;

namespace NotionOutput.Models;

internal class ProductVersionObject
{
    public string? NotionId { get; set; }
    public VersionType Version { get; set; }
    public Product Product { get; set; }

    public ProductVersionObject(Product product)
    {
        Product = product;
    }
}
