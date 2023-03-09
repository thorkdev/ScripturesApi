using Microsoft.AspNetCore.Http.Extensions;
using ScripturesApi.Extensions;
using ScripturesApi.Services.Abstract;

namespace ScripturesApi.Middleware;

public class ApiKeyMiddleware
{
    private readonly RequestDelegate _next;

    private const string _apiKey = "x-api-key";

    public ApiKeyMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, IConfiguration configuration, IClientService clientService, IIpService ipService)
    {
        var hasKey = context.Request.Headers.TryGetValue(_apiKey, out var key);

        if (!hasKey)
        {
            await context.Response.UpdateResponse(StatusCodes.Status401Unauthorized, "Api key was not provided.");

            return;
        }

        var keyIsValid = Guid.TryParse(key, out var guid);

        if (!keyIsValid)
        {
            await context.Response.UpdateResponse(StatusCodes.Status400BadRequest, "Api key is not valid.");

            return;
        }

        var keyIsActive = await clientService.ApiKeyIsActiveAsync(guid);

        if (!keyIsActive)
        {
            await context.Response.UpdateResponse(StatusCodes.Status403Forbidden, "Api key is inactive.");

            return;
        }

        // throttle

        var dailyRequests = await ipService.GetDailyRequestsAsync(guid);

        _ = int.TryParse(configuration["DailyRequestLimit"], out var dailyRequestLimit);

        if (dailyRequests >= dailyRequestLimit)
        {
            context.Response.Headers.RetryAfter = DateTime.UtcNow.Date.ToString("r"); // format gmt

            await context.Response.UpdateResponse(StatusCodes.Status429TooManyRequests, "Too many requests.");

            return;
        }

        // log

        var ip = context.Connection.RemoteIpAddress?.ToString();

        if (!string.IsNullOrEmpty(ip))
        {
            var requestUri = context.Request.GetEncodedUrl();

            await ipService.LogIpAsync(guid, ip, requestUri);
        }

        await _next(context);
    }
}
