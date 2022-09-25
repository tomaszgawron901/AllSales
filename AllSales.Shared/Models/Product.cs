using AllSales.Shared.Enums;

namespace AllSales.Shared.Models;

public sealed class Product
{
    public string ShopId { get; set; }
    public string Name { get; }
    public Uri Uri { get; }
    public double NormalPrice { get; }
    public double SalePrice { get; }
    public string Shop { get; }
    public GenderType? Gender { get; }
    

    public Product(string shopId, string name, Uri uri, double normalPrice, double salePrice, string shop, GenderType? gender)
    {
        ShopId = shopId;
        Name = name;
        Uri = uri;
        NormalPrice = normalPrice;
        SalePrice = salePrice;
        Shop = shop;
        Gender = gender;
    }

    public static string CreateProductId(string shop, string shopId)
    {
        return $"{shop}_{shopId}";
    }
}