using ApiGatewayService.Repositories;

namespace ApiGatewayService.Middlewares
{
    public class AuthorizationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly List<String> ALLOWED_PATHS;
        private IAuthenticationService _authenticationService;

        public AuthorizationMiddleware(RequestDelegate next
            ,IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
            _next = next;
            ALLOWED_PATHS =new List<String>{
                "/ApiGateway/login"
                ,"/ApiGateway/signup"
            };
        }

        public async Task InvokeAsync(HttpContext context)
        {
            Console.WriteLine($"middleware struck :{context.Request.Path}");

            if (ALLOWED_PATHS.Contains(context.Request.Path))
            {
                await _next(context);
                return;
            }

            string bearerToken = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            bool isValid = await _authenticationService.Validate(bearerToken);

            if (!isValid)
            {
                SetUnauthorizedResponse(context);
                return;
            }

            await _next(context);
            return;


        }


        private static void SetUnauthorizedResponse(HttpContext context)
        {
            context.Response.StatusCode = 401;
            context.Response.WriteAsync("Unauthorized");
        }
    }

    public static class AuthorizationMiddlewareExtensions
    {
        public static IApplicationBuilder UseAuthorizationMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AuthorizationMiddleware>();
        }
    }
}
