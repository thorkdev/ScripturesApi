using Microsoft.AspNetCore.Mvc;
using ScripturesApi.Domain.Entities;
using ScripturesApi.Services.Abstract;

namespace ScripturesApi.Controllers;

#if DEBUG
[ApiExplorerSettings(IgnoreApi = false)]
#else
    [ApiExplorerSettings(IgnoreApi = true)]
#endif 

[ApiController]
[Route("api/v1/[controller]")]
public class ClientKeysController : ControllerBase
{
    private readonly ILogger<ClientKeysController> _logger;
    private readonly IClientService _clientService;

    public ClientKeysController(ILogger<ClientKeysController> logger,
        IClientService clientService)
    {
        _logger = logger;
        _clientService = clientService;
    }

    [HttpGet("generate")]
    public async Task<IActionResult> GenerateClientKey()
    {
        try
        {
            var isRequestValid = await IsRequestValid();

            if (!isRequestValid)
            {
                return Unauthorized();
            }

            var key = await _clientService.CreateApiKeyAsync();

            if (key == null)
            {
                return BadRequest("Failed to generate new client api key.");
            }

            return Ok(key);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error generating new client api key. Message: {ex.Message}");

            return BadRequest(ex.Message);
        }
    }

    [HttpGet("toggle/{apiKey:guid}")]
    public async Task<IActionResult> ToggleClientKey(Guid apiKey)
    {
        try
        {
            var isRequestValid = await IsRequestValid();

            if (!isRequestValid)
            {
                return Unauthorized();
            }

            await _clientService.ToggleApiKeyAsync(apiKey);

            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error toggling client api key. Message: {ex.Message}");

            return BadRequest(ex.Message);
        }
    }

    private async Task<bool> IsRequestValid()
    {
        if (!HttpContext.Request.Headers.ContainsKey("x-api-key"))
        {
            return false;
        }

        var apiKey = Guid.Parse(HttpContext.Request.Headers["x-api-key"].ToString());

        var clientRole = await _clientService.GetClientRoleAsync(apiKey);

        return clientRole == ClientKeyRole.Admin;
    }
}
