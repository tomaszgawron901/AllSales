using AllSales.Shared.Enums;

namespace AllSales.Shared.Models;

public class Product
{
    public string Name { get; }
    public Uri Uri { get; }
    public decimal NormalPrice { get; }
    public decimal SalePrice { get; }
    public string Shop { get; }
    public GenderType? Gender { get; }

    public Product(string name, Uri uri, decimal normalPrice, decimal salePrice, string shop, GenderType? gender)
    {
        Name = name;
        Uri = uri;
        NormalPrice = normalPrice;
        SalePrice = salePrice;
        Shop = shop;
        Gender = gender;
    }
    
}