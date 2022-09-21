using AllSales.Shared.Collections;
using AllSales.Shared.Enums;
using AllSales.Shared.Models;
using HmInput.Models;

namespace HmInput.Mapping;

internal static class ProductMapper
{
    public static bool TryMapToProduct(HmProduct hmProduct, out Product? product)
    {
        if (hmProduct.Link is null || hmProduct.Price is null || hmProduct.RedPrice is null)
        {
            product = null;
            return false;
        }
        
        string name = hmProduct.Title ?? string.Empty;
        Uri uri = new Uri($"{ApiEndpoints.HmBaseUri}{hmProduct.Link}");
        decimal? price = PriceMapper.MapToDecimal(hmProduct.Price);
        decimal? salePrice = PriceMapper.MapToDecimal(hmProduct.RedPrice);

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
                else if(categories[0].Equals("women"))
                {
                    gender = GenderType.Female;
                }
            }
        }
        

        product = new Product(name, uri, price.Value, salePrice.Value, Shops.HM, gender);
        return true;
    }
}
