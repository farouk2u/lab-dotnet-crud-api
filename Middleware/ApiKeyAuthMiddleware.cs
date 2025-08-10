using System.Security.Claims;

namespace dotnet_crud_api.Middleware
{
    public class ApiKeyAuthMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ApiKeyAuthMiddleware> _logger;
        private readonly string _apiKey;

        public ApiKeyAuthMiddleware(
            RequestDelegate next,
            ILogger<ApiKeyAuthMiddleware> logger,
            IConfiguration configuration)
        {
            _next = next;
            _logger = logger;
            _apiKey = configuration["Authentication:ApiKey"] ?? "default-api-key-for-development";
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.Headers.TryGetValue("X-API-Key", out var extractedApiKey))
            {
                _logger.LogWarning("API Key was not provided. Request path: {Path}", context.Request.Path);
                context.Response.StatusCode = 401; // Unauthorized
                await context.Response.WriteAsync("API Key is required");
                return;
            }

            if (!_apiKey.Equals(extractedApiKey))
            {
                _logger.LogWarning("Invalid API Key provided. Request path: {Path}", context.Request.Path);
                context.Response.StatusCode = 401; // Unauthorized
                await context.Response.WriteAsync("Invalid API Key");
                return;
            }

            // If we get here, the API key is valid
            // Create a simple identity and add it to the request
            var identity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, "ApiUser"),
                new Claim(ClaimTypes.Role, "ApiAccess"),
            }, "ApiKeyAuth");

            context.User = new ClaimsPrincipal(identity);

            _logger.LogInformation("Valid API key authentication for request: {Path}", context.Request.Path);
            await _next(context);
        }
    }

    // Extension method to make it easier to register the middleware
    public static class ApiKeyAuthMiddlewareExtensions
    {
        public static IApplicationBuilder UseApiKeyAuth(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ApiKeyAuthMiddleware>();
        }
    }
}
