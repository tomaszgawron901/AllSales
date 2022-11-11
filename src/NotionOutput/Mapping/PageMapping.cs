using AllSales.Shared.Enums;
using AllSales.Shared.Models;
using Notion.Client;
using NotionOutput.Collections;
using System.Diagnostics.CodeAnalysis;

namespace NotionOutput.Mapping;


internal static class PageMapping
{
    public static bool TryMapToProduct(this Page page, [MaybeNullWhen(false)] out Product product)
    {
        product = null;
        
        var id = (page.Properties[ProductPropertyNames.Id] as RichTextPropertyValue)?.RichText.FirstOrDefault()?.PlainText;
        if (id is null)
        {
            return false;
        }

        var name = (page.Properties[ProductPropertyNames.Name] as TitlePropertyValue)?.Title.FirstOrDefault()?.PlainText;
        if (name is null)
        {
            return false;
        }
        
        var normalPrice = (page.Properties[ProductPropertyNames.NormalPrice] as NumberPropertyValue)?.Number;
        if (normalPrice is null)
        {
            return false;
        }

        var salePrice = (page.Properties[ProductPropertyNames.SalePrice] as NumberPropertyValue)?.Number;
        if (salePrice is null)
        {
            return false;
        }

        var url = (page.Properties[ProductPropertyNames.URL] as UrlPropertyValue)?.Url;
        if (url is null)
        {
            return false;
        }

        var shop = (page.Properties[ProductPropertyNames.Shop] as SelectPropertyValue)?.Select.Name;
        if(shop is null)
        {
            return false;
        }

        var genderProp = (page.Properties[ProductPropertyNames.Gender] as SelectPropertyValue);
        if(genderProp is null)
        {
            return false;
        }
        
        GenderType? gender = GenderMapping.MapPropertyToGender(genderProp);

        product = new Product(id, name, new Uri(url), normalPrice.Value, salePrice.Value, shop, gender);
        return true;
    }
}
