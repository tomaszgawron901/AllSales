using AllSales.Shared.Models;
using AllSales.Shared.Services;
using HmInput.Mapping;
using HmInput.Models;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace HmInput;

public class HmInputService : IInputService
{
    private readonly HttpClient _httpClient;
    private readonly Uri baseUri = new Uri("https://www2.hm.com");
    private readonly int fetchBatchSize = 500;

    public HmInputService(HttpClient client)
    {
        _httpClient = client;

    }

    public async Task<List<Product>> GetSaleProducts()
    {

        _httpClient.DefaultRequestHeaders.Accept.Clear();
        _httpClient.DefaultRequestHeaders.Accept.Add( new MediaTypeWithQualityHeaderValue("*/*"));
        _httpClient.DefaultRequestHeaders.Add("User-Agent", "AllSales/0.0");

        List<Product> saleProducts = new List<Product>();
        HmResponse? response;
        int productGet = 0;
        int productTotal = 0;
        do
        {
            try
            {
                response = await _httpClient.GetFromJsonAsync<HmResponse>($"{ApiEndpoints.HmBaseUri}{ApiEndpoints.AllMenProductsRelativeUri}?offset={productGet}&page-size={fetchBatchSize}");
            }
            catch (Exception)
            {
                break;
            }
            if (response is null || response.Products is null || response.ItemsShown is null || response.Total is null)
            {
                break;
            }

            productGet = response.ItemsShown.Value;
            productTotal = response.Total.Value;

            foreach (var hmProduct in response.Products)
            {
                if(ProductMapper.TryMapToProduct(hmProduct, out var product) && product is not null)
                {
                    saleProducts.Add(product);
                }
            }
        }
        while (productGet < productTotal);

        return saleProducts;
    }
}