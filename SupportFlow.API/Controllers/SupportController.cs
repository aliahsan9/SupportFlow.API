using Microsoft.AspNetCore.Mvc;
using SupportFlow.API.Agents;
using SupportFlow.API.Models;

namespace SupportFlow.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SupportController : ControllerBase
{
    private readonly SupportAgent _supportAgent;

    public SupportController(SupportAgent supportAgent)
    {
        _supportAgent = supportAgent;
    }

    [HttpPost("ask")]
    public async Task<IActionResult> Ask([FromBody] ChatRequest request)
    {
        var response = await _supportAgent.AskAsync(
    request.SessionId,
    request.Message);

        return Ok(response);
    }
}