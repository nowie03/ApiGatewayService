using ApiGatewayService.Models;
using ApiGatewayService.Repositories;
using Newtonsoft.Json;

namespace ApiGatewayService.Services
{
    public class UserService : IUserService
    {
        private HttpClient _httpClient;
        private readonly string BASE_ADDRESS = "http://localhost:5005/api/Users/address";

        public UserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> DeleteUserAddressOfUserAsync(int userAddressId)
        {
            try
            {
                var response=await _httpClient.DeleteAsync($"{BASE_ADDRESS}?id={userAddressId}");

                if (response.IsSuccessStatusCode)
                    return true;

                return false;
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<IEnumerable<UserAddress>> GetAddressesOfUserAsync(int userId)
        {
            try{

                var response=await _httpClient.GetAsync($"{BASE_ADDRESS}?userId={userId}");

                if (response.IsSuccessStatusCode)
                {
                    string content=await response.Content.ReadAsStringAsync();
                    IEnumerable<UserAddress> addresses = JsonConvert.DeserializeObject<IEnumerable<UserAddress>>(content);

                    return addresses;

                }

                return Enumerable.Empty<UserAddress>();
            }
            catch (Exception ex){
                Console.WriteLine(ex.Message);
                return Enumerable.Empty<UserAddress>();

            }
        }

        public async Task<UserAddress?> PostUserAddressOfUserAsync(UserAddress userAddress)
        {
            try{
                var response =await _httpClient.PostAsJsonAsync(BASE_ADDRESS, userAddress);

                if (response.IsSuccessStatusCode)
                    return userAddress;

                return null;
            }catch(Exception ex){
                Console.WriteLine(ex.Message);
                return null;
            }
        }

       
    }
}
