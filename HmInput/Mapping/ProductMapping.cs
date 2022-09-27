using AllSales.Shared.Collections;
using AllSales.Shared.Enums;
using AllSales.Shared.Models;
using HmInput.Models;
using System.Diagnostics.CodeAnalysis;

namespace HmInput.Mapping;

internal static class ProductMapping
{
    public static bool TryMapToProduct(HmProduct hmProduct,[MaybeNullWhen(false)] out Product product)
    {
        if (hmProduct.Link is null || hmProduct.Price is null || hmProduct.ArticleCode is null)
        {
            product = null;
            return false;
        }
        
        string name = hmProduct.Title ?? string.Empty;
        Uri uri = new Uri($"{ApiEndpoints.HmBaseUri}{hmProduct.Link}");
        double? price = PriceMapper.MapToDouble(hmProduct.Price);

        double? salePrice = null;
        if (!string.IsNullOrWhiteSpace(hmProduct.RedPrice))
        {
            salePrice = PriceMapper.MapToDouble(hmProduct.RedPrice);
        }
        else if (!string.IsNullOrWhiteSpace(hmProduct.YellowPrice))
        {
            salePrice = PriceMapper.MapToDouble(hmProduct.YellowPrice);
        }

        if (price is null || salePrice is null)
        {
            product = null;
            return false;
        }

        GenderType? gender = null;
        if (hmProduct.Category is not null)
        {
            var categories = hmProduct.Category.Split('_');

            if (categories.Length > 0)
            {
                if (categories[0].Equals("men"))
                {
                    gender = GenderType.Male;
                }
                else if(categories[0].Equals("ladies"))
                {
                    gender = GenderType.Female;
                }
            }
        }
        

        product = new Product(Product.CreateProductId(Shops.HM, hmProduct.ArticleCode), name, uri, price.Value, salePrice.Value, Shops.HM, gender);
        return true;
    }
}
