using ApiGatewayService.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;

namespace ApiGatewayService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private IHttpClientFactory _httpClientFactory;
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }
        [HttpGet]
        [Route("users")]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers(int skip,int limit)
        {
            var httpClient=_httpClientFactory.CreateClient();

            var content = await httpClient.GetFromJsonAsync<IEnumerable<User>>($"http://localhost:5005/api/Users?limit={limit}&skip={skip}");

            return Ok(content);

        }
        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}