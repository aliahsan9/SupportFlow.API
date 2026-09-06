using Microsoft.AspNetCore.Mvc;
using SupportFlow.API.Models;
using SupportFlow.API.Services;

namespace SupportFlow.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SupportController : ControllerBase
{
    private readonly MultiAgentService _multiAgentService;

    public SupportController(MultiAgentService multiAgentService)
    {
        _multiAgentService = multiAgentService;
    }

    [HttpPost("ask")]
    public async Task<IActionResult> Ask(
        [FromBody] ChatRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Message))
        {
            return BadRequest("Message cannot be empty.");
        }

        var response =
            await _multiAgentService.AskAsync(request.Message);

        return Ok(response);
    }
}