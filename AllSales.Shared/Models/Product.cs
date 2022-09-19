namespace AllSales.Shared.Models;

public class Product
{
    public string Name { get; }
    public Uri Uri { get; }
    public decimal NormalPrice { get; }
    public decimal SalePrice { get; }

    public Product(string name, Uri uri, decimal normalPrice, decimal salePrice)
    {
        Name = name;
        Uri = uri;
        NormalPrice = normalPrice;
        SalePrice = salePrice;
    }

}