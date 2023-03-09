namespace ScripturesApi.Extensions;

public static class HttpExtensions
{
    public static async Task UpdateResponse(this HttpResponse response, int statusCode, string message)
    {
        response.StatusCode = statusCode;

        await response.WriteAsync(message);
    }
}
