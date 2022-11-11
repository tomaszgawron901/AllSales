using AllSales.Shared.Models;
using Notion.Client;
using NotionOutput.Collections;

namespace NotionOutput.Mapping;

internal static class ProductMapping
{
    public static IDictionary<string, PropertyValue> MapToProperties(Product product)
    {
        var dictionary = new Dictionary<string, PropertyValue>
        {
            {
                ProductPropertyNames.Id,
                new RichTextPropertyValue
                {
                    RichText = new List<RichTextBase> { new RichTextText { PlainText = product.ShopId, Text = new Text { Content = product.ShopId } } },
                }
            },
            {
                ProductPropertyNames.Name,
                new TitlePropertyValue
                {
                    Title = new List<RichTextBase> { new RichTextText { PlainText = product.Name, Text = new Text { Content = product.Name } } },
                }
            },
            {
                ProductPropertyNames.NormalPrice,
                new NumberPropertyValue
                {
                    Number = product.NormalPrice,
                }
            },
            {
                ProductPropertyNames.SalePrice,
                new NumberPropertyValue
                {
                    Number = product.SalePrice,
                }
            },
            {
                ProductPropertyNames.URL,
                new UrlPropertyValue
                {
                    Url = product.Uri.ToString()
                }
            },
            {
                ProductPropertyNames.Shop,
                new SelectPropertyValue
                {
                    Select = new SelectOption { Name = product.Shop },
                }
            }
        };

        if (product.Gender is not null)
        {
            dictionary.Add(ProductPropertyNames.Gender, GenderMapping.MapGenderToProperty(product.Gender.Value));
        }

        return dictionary;
    }
}
