using ApiGatewayService.Models;
using ApiGatewayService.Repositories;
using Newtonsoft.Json;

namespace ApiGatewayService.Services
{
    public class PaymentService : IPaymentService
    {
        private HttpClient _httpClient;
        private readonly string BASE_ADDRESS = "http://localhost:5025/api/Payments";

        public PaymentService(HttpClient httpClient)
        {   
            _httpClient = httpClient;

        }

        public async Task<Payment?> GetPaymentForOrderAsync(int orderId)
        {
            try
            {
                var response =await _httpClient.GetAsync($"{BASE_ADDRESS}/order?orderId={orderId}");

                if(response.IsSuccessStatusCode)
                {
                    string content =await response.Content.ReadAsStringAsync();

                    Payment payment = JsonConvert.DeserializeObject<Payment>(content);

                    return payment;
                }

                return null;
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<Payment?> UpdateStatusForPaymentAsync(int paymentId, Payment payment)
        {
           try{
                var response = await _httpClient.PutAsJsonAsync($"{BASE_ADDRESS}/{paymentId}", payment);

                if (response.IsSuccessStatusCode)
                {
                    return payment;
                }
                return null;
            }
            catch(Exception ex){
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
