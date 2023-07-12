using ApiGatewayService.Models;
using ApiGatewayService.Repositories;
using Newtonsoft.Json;

namespace ApiGatewayService.Services
{
    public class PaymentService : IPaymentService
    {
        private HttpClient _httpClient;
        private readonly string BASE_ADDRESS;

        public PaymentService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            BASE_ADDRESS = configuration.GetConnectionString("payment-service");

        }

        public async Task<Payment?> GetPaymentForOrderAsync(int orderId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{BASE_ADDRESS}/order?orderId={orderId}");

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();

                    Payment payment = JsonConvert.DeserializeObject<Payment>(content);

                    return payment;
                }

                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<Payment?> UpdateStatusForPaymentAsync(int paymentId, Payment payment)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"{BASE_ADDRESS}/{paymentId}", payment);

                if (response.IsSuccessStatusCode)
                {
                    return payment;
                }
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
