using ApiGatewayService.Models;
using ApiGatewayService.Repositories;
using Newtonsoft.Json;

namespace ApiGatewayService.Services
{
    public class UserService : IUserService
    {
        private HttpClient _httpClient;
        private readonly string BASE_ADDRESS;

        public UserService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            BASE_ADDRESS = configuration.GetConnectionString("user-service");

        }

        public async Task<bool> DeleteUserAddressOfUserAsync(int userAddressId)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"{BASE_ADDRESS}/address?id={userAddressId}");

                if (response.IsSuccessStatusCode)
                    return true;

                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<IEnumerable<UserAddress>> GetAddressesOfUserAsync(int userId)
        {
            try
            {

                var response = await _httpClient.GetAsync($"{BASE_ADDRESS}/address?userId={userId}");

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    IEnumerable<UserAddress> addresses = JsonConvert.DeserializeObject<IEnumerable<UserAddress>>(content);

                    return addresses;

                }

                return Enumerable.Empty<UserAddress>();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Enumerable.Empty<UserAddress>();

            }
        }

        public async Task<UserAddress?> PostUserAddressOfUserAsync(UserAddress userAddress)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync($"{BASE_ADDRESS}/address",userAddress);

                if (response.IsSuccessStatusCode)
                    return userAddress;

                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<User?> CreateUserAsync(User user)
        {
            try {
                var response = await _httpClient.PostAsJsonAsync(BASE_ADDRESS, user);

                if (response.IsSuccessStatusCode)
                {
                    return user;
                }

                return null;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<User?> GetUserAsync(int userId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{BASE_ADDRESS}/userId");

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    User user = JsonConvert.DeserializeObject<User>(content);

                    return user;
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
