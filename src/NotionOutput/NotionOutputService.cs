using AllSales.Application.Services;
using AllSales.Shared.Models;
using Notion.Client;
using NotionOutput.Enums;
using NotionOutput.Mapping;
using NotionOutput.Models;

namespace NotionOutput;

public class NotionOutputService : IOutputService
{
    private readonly INotionClient _notionClient;
    private readonly string databaseId = "9ebd82cb4230470b997a29716c413ee8";
    private readonly ProductRepository _productRepository = new();
    private readonly List<string> brokenPageIds = new();

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
    
    public void AddOrUpdate(Product product)
    {
        _productRepository.AddOrUpdate(product);
    }

    public async Task Pull()
    {
        _productRepository.Clear();
        brokenPageIds.Clear();
        var paginatedOutput = await _notionClient.Databases.QueryAsync(databaseId, new DatabasesQueryParameters { });

        while (paginatedOutput is not null)
        {
            foreach (var page in paginatedOutput.Results)
            {
                if(page is null)
                {
                    continue;
                }

                if(page.TryMapToProduct(out var product))
                {
                    _productRepository.Put(new ProductVersionObject(product) { NotionId = page.Id, Version = VersionType.Old });
                }
                else
                {
                    brokenPageIds.Add(page.Id);
                }
            }

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

    public Task Push()
    {
        List<Task> tasks = new()
        {
            DeletePagesById(brokenPageIds),
        };
        foreach (var versionObj in _productRepository.GetProducts())
        {
            if (versionObj.Version is VersionType.Old )
            {
                if(versionObj.NotionId is not null)
                {
                     tasks.Add(DeletePageById(versionObj.NotionId));
                }
            }
            else if(versionObj.Version is VersionType.New)
            {
                tasks.Add(AddProduct(versionObj.Product));
            }
            else if(versionObj.Version is VersionType.Updated)
            {
                if (versionObj.NotionId is not null)
                {
                    tasks.Add(UpdateProduct(versionObj.NotionId, versionObj.Product));
                } 
            }
        }
        return Task.WhenAll(tasks);
    }

    private Task DeletePageById(string pageId)
    {
        var parameters = new PagesUpdateParameters { Archived = true };
        return _notionClient.Pages.UpdateAsync(pageId, parameters);
    }

    private Task DeletePagesById(IEnumerable<string> pageIds)
    {
        var parameters = new PagesUpdateParameters { Archived = true };
        IEnumerable<Task> tasks = pageIds.Select(pageId => _notionClient.Pages.UpdateAsync(pageId, parameters));
        return Task.WhenAll(tasks);
    }

    private Task UpdateProduct(string pageId, Product product)
    {
        var parameters = new PagesUpdateParameters
        {
            Properties = ProductMapping.MapToProperties(product)
        };
        return _notionClient.Pages.UpdateAsync(pageId, parameters);
    }

    private Task AddProduct(Product product)
    {
        var parameters = new PagesCreateParameters
        {
            Parent = new DatabaseParentInput { DatabaseId = databaseId },
            Properties = ProductMapping.MapToProperties(product),
        };
        return _notionClient.Pages.CreateAsync(parameters);
    }
}