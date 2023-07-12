using ApiGatewayService.Models;
using ApiGatewayService.Repositories;
using ApiGatewayService.ResponseModels;
using Newtonsoft.Json;

namespace ApiGatewayService.Services
{
    public class InventoryService : IInventoryService
    {
        private HttpClient _httpClient;
        private readonly string BASE_ADDRESS ; 

        public InventoryService(HttpClient httpClient,IConfiguration configuration)
        {
            _httpClient = httpClient;
            BASE_ADDRESS = configuration.GetConnectionString("inventory-service");
        }
        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            try {
                var response = await _httpClient.GetAsync($"{BASE_ADDRESS}/category");

                if (response.IsSuccessStatusCode)
                {
                    var content = response.Content.ReadAsStringAsync().Result;

                    IEnumerable<Category> categories = JsonConvert.DeserializeObject<IEnumerable<Category>>(content);

                    return categories;
                }

                else return Enumerable.Empty<Category>();

            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
                return Enumerable.Empty<Category>();
            }
        }

        public async Task<ProductGetResponse?> GetProductAsync(int productId)
        {
            try {
                var response = await _httpClient.GetAsync($"{BASE_ADDRESS}/{productId}");

                if (response.IsSuccessStatusCode)
                {
                    string content=response.Content.ReadAsStringAsync().Result;
                    ProductGetResponse product = JsonConvert.DeserializeObject<ProductGetResponse>(content);

                    return product;
                }

                return null;

                

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        

        public async  Task<IEnumerable<ProductGetResponse>> GetProductsAsync(int limit, int skip)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{BASE_ADDRESS}?limit={limit}&skip={skip}");

                if (response.IsSuccessStatusCode)
                {
                    var content = response.Content.ReadAsStringAsync().Result;

                    IEnumerable<ProductGetResponse> products = JsonConvert.DeserializeObject<IEnumerable<ProductGetResponse>>(content);

                    return products;
                }

                else return Enumerable.Empty<ProductGetResponse>();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Enumerable.Empty<ProductGetResponse>();
            }
        }
    }
}
