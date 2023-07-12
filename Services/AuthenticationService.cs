
using ApiGatewayService.Models;
using ApiGatewayService.Repositories;
using ApiGatewayService.RequestModels;
using ApiGatewayService.ResponseModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;

namespace ApiGatewayService.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private HttpClient _httpClient;
        private readonly string BASE_ADDRESS;
     
        public AuthenticationService(HttpClient httpClient,IConfiguration configuration)
        {
            BASE_ADDRESS = configuration.GetConnectionString("authentication-service");
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

        public async Task<bool> Validate(string bearerToken)
        {
            try {
                var response = await _httpClient.GetAsync($"{BASE_ADDRESS}/validate?token={bearerToken}");

                Console.WriteLine(response);

                if (response.IsSuccessStatusCode)
                {
                    var content = response.Content.ReadAsStringAsync().Result;

                    bool result = JsonConvert.DeserializeObject<bool>(content);

                    return result;
                }

                return false;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
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
