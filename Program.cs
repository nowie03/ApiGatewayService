using ApiGatewayService.Middlewares;
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
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowSameDomain", builder =>
                {
                    builder.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });

            //strongly typed httpclients inject as services
            builder.Services.AddHttpClient<IAuthenticationService, AuthenticationService>();
            builder.Services.AddHttpClient<IInventoryService, InventoryService>();
            builder.Services.AddHttpClient<IReviewService, ReviewService>();
            builder.Services.AddHttpClient<IOrderService, OrderService>();
            builder.Services.AddHttpClient<ICartService, CartService>();    
            builder.Services.AddHttpClient<IPaymentService,PaymentService>();
            builder.Services.AddHttpClient<IUserService, UserService>();



            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            // Configure the HTTP request pipeline.
            var app = builder.Build();
         
                app.UseSwagger();
                app.UseSwaggerUI();

           

            app.UseCors("AllowSameDomain");


            app.UseAuthorization();


            app.MapControllers();
            app.UseAuthorizationMiddleware();
            app.Run();
        }
    }
}