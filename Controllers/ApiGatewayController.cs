using ApiGatewayService.Models;
using ApiGatewayService.Repositories;
using ApiGatewayService.RequestModels;
using ApiGatewayService.ResponseModels;
using Microsoft.AspNetCore.Mvc;

namespace ApiGatewayService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ApiGatewayController : Controller
    {
        private IAuthenticationService _authenticationService;

        public ApiGatewayController( IAuthenticationService authenticationService)
        {
           
            _authenticationService = authenticationService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<LoginResponse>> Login(LoginRequest request)
        {

            var response = await _authenticationService.Login(request);

            if (response == null) return BadRequest();

            return Ok(response);
        }

        [HttpPost]
        [Route("signup")]
        public async Task<ActionResult<User?>> Signup(User user)
        {

            var response = await _authenticationService.SignUp(user);

            if(response==null)return BadRequest();

            return Ok(response);
        }

    }
}
