using ApiGatewayService.Repositories;
using ApiGatewayService.Services;

namespace ApiGatewayService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.


            builder.Services.AddHttpClient<IAuthenticationService, AuthenticationService>();
            builder.Services.AddHttpClient<IInventoryService, InventoryService>();
            builder.Services.AddHttpClient<IReviewService, ReviewService>();
            builder.Services.AddHttpClient<IOrderService, OrderService>();
            builder.Services.AddHttpClient<ICartService, CartService>();    


            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
         
                app.UseSwagger();
                app.UseSwaggerUI();
            

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}