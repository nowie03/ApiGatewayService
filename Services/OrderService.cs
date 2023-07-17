using ApiGatewayService.Models;
using ApiGatewayService.Repositories;
using Newtonsoft.Json;

namespace ApiGatewayService.Services
{
    public class OrderService : IOrderService
    {
        private HttpClient _httpClient;
        private readonly string BASE_ADDRESS;
        public OrderService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            BASE_ADDRESS = configuration.GetConnectionString("order-service");
        }

        public async Task<bool> CheckOutOrderAsync(int orderId)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync($"{BASE_ADDRESS}?orderId={orderId}", orderId);
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }

                return false;

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<bool> DeleteOrderForUserAsync(int orderId)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"{BASE_ADDRESS}/{orderId}");

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<IEnumerable<Order>> GetOrdersForUserAsync(int userId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{BASE_ADDRESS}?userId={userId}");

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();

                    IEnumerable<Order> orders = JsonConvert.DeserializeObject<IEnumerable<Order>>(content);

                    return orders;
                }
                return Enumerable.Empty<Order>();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Enumerable.Empty<Order>();
            }
        }

        public async Task<Order?> PostOrderForUserAsync(Order order)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync($"{BASE_ADDRESS}", order);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();

                    Order orderResponse = JsonConvert.DeserializeObject<Order>(content);

                    return orderResponse;
                }

                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<Order?> PutOrderForUserAsync(int orderId, Order order)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"{BASE_ADDRESS}/{orderId}", order);
                if (response.IsSuccessStatusCode)
                {
                    return order;
                }

                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<Order?> GetOrderAsync(int orderId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{BASE_ADDRESS}/{orderId}");

                if (response.IsSuccessStatusCode)
                {
                    string content=await response.Content.ReadAsStringAsync();

                    Order order= JsonConvert.DeserializeObject<Order>(content);

                    return order;
                }

                return null;

            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
