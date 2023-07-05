
using ApiGatewayService.Models;
using ApiGatewayService.Repositories;
using ApiGatewayService.RequestModels;
using ApiGatewayService.ResponseModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ApiGatewayService.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private HttpClient _httpClient;
        private readonly string BASE_ADDRESS = "http://localhost:5035/api/Users";
        public AuthenticationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<LoginResponse?> Login(LoginRequest request)
        {
            try
            {
              

                var response = await _httpClient.PostAsJsonAsync($"{BASE_ADDRESS}/login", request);

                // Deserialize the response
                if (response.IsSuccessStatusCode)
                {
                    string content=response.Content.ReadAsStringAsync().Result;
                    Console.WriteLine(content);
                    LoginResponse loginResponse=JsonConvert.DeserializeObject<LoginResponse>(content);
                    return loginResponse;
                }

                return null;
              

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public Task Validate()
        {
            throw new NotImplementedException();
        }

        public async Task<User?> SignUp(User user)
        {
            try
            {


                var response = await _httpClient.PostAsJsonAsync($"{BASE_ADDRESS}/signup", user);

                // Deserialize the response
                if (response.IsSuccessStatusCode)
                {
                    return user;
                   
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
