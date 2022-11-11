using AllSales.Shared.Models;
using NotionOutput.Enums;
using NotionOutput.Models;
using System.Diagnostics.CodeAnalysis;

namespace NotionOutput;

internal sealed class ProductRepository
{

    private readonly Dictionary<string, ProductVersionObject> _products;

    public ProductRepository()
    {
        _products = new();
    }
    
    public void Put(ProductVersionObject versionObj)
    {
        _products.Add(versionObj.Product.ShopId, versionObj);
    }

    public void Clear()
    {
        _products.Clear();
    }

    public void AddOrUpdate(Product product)
    {
        if (_products.Remove(product.ShopId, out var existingVersionObj))
        {
            _products.Add(product.ShopId, new ProductVersionObject(product)
            {
                Version = ProductsAreIdentical(product, existingVersionObj.Product)
                    ? VersionType.NotChanged
                    : VersionType.Updated,
                NotionId = existingVersionObj.NotionId,
            });
        }
        else
        {
            _products.Add(product.ShopId, new ProductVersionObject(product) { Version = VersionType.New });
        }
    }

    public IEnumerable<ProductVersionObject> GetProducts()
    {
        return _products.Values;
    }

    private static bool ProductsAreIdentical(Product a, Product b)
    {
        return a.ShopId.Equals(b.ShopId)
            && a.Shop.Equals(b.Shop)
            && a.Name.Equals(b.Name)
            && a.NormalPrice.Equals(b.NormalPrice)
            && a.SalePrice.Equals(b.SalePrice)
            && a.Gender.Equals(b.Gender)
            && a.Uri.Equals(b.Uri);
    }
}
