using ApiGatewayService.Models;
using ApiGatewayService.RequestModels;
using ApiGatewayService.ResponseModels;

namespace ApiGatewayService.Repositories
{
    public interface IAuthenticationService
    {
        public Task<bool> Validate(string token);


        public Task<LoginResponse?> Login(LoginRequest request);


        public Task<User?> SignUp(User user);


    }
}
