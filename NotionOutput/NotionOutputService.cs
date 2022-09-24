using AllSales.Shared.Models;
using AllSales.Shared.Services;
using Notion.Client;
using NotionOutput.Mapping;

namespace NotionOutput;

public class NotionOutputService : IOutputService
{
    private readonly INotionClient _notionClient;
    private readonly string databaseId = "9ebd82cb4230470b997a29716c413ee8";

    public NotionOutputService()
    {
        _notionClient = NotionClientFactory.Create(new ClientOptions
        {
            AuthToken = Environment.GetEnvironmentVariable("Notion:AuthToken"),
        });
    }

    public NotionOutputService(INotionClient notionClient)
    {
        _notionClient = notionClient;
    }

    public async Task AddProducts(List<Product> products)
    {
        foreach (var product in products)
        {
            var builder = PagesCreateParametersBuilder.Create(new DatabaseParentInput
            {
                DatabaseId = databaseId,
            })
            .AddProperty("Name", new TitlePropertyValue
            {
                Title = new List<RichTextBase> { new RichTextText { PlainText = product.Name, Text = new Text { Content = product.Name } } },
            })
            .AddProperty("NormalPrice", new NumberPropertyValue {
                Number = (double?)product.NormalPrice,
            })
            .AddProperty("SalePrice", new NumberPropertyValue
            {
                Number = (double?)product.SalePrice,
            })
            .AddProperty("URL", new UrlPropertyValue
            {
                Url = product.Uri.ToString()
            })
            .AddProperty("Shop", new SelectPropertyValue
            {
                Select = new SelectOption { Name = product.Shop },
            });

            if (product.Gender is not null)
            {
                builder = builder.AddProperty("Gender", NotionMapping.MapGenderToProperty(product.Gender.Value));
            }

            var parameters = builder.Build();
            await _notionClient.Pages.CreateAsync(parameters);
        }
    }

    public async Task ClearProducts()
    {
        var paginatedOutput = await _notionClient.Databases.QueryAsync(databaseId, new DatabasesQueryParameters { });

        while (paginatedOutput is not null)
        {
            await DeletePages(paginatedOutput.Results);

            if (!paginatedOutput.HasMore) { break; }
            
            paginatedOutput = await _notionClient.Databases.QueryAsync(
                databaseId,
                new DatabasesQueryParameters
                {
                    StartCursor = paginatedOutput.NextCursor
                }
            );
        }
    }

    private async Task DeletePages(List<Page> pages)
    {
        foreach (var page in pages)
        {
            await _notionClient.Pages.UpdateAsync(page.Id, new PagesUpdateParameters { Archived = true });
        }
    }
}