using Microsoft.AspNetCore.Mvc;
using SupportFlow.API.Agents;

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
    public async Task<IActionResult> Ask([FromBody] string message)
    {
        var response = await _supportAgent.AskAsync(message);

        return Ok(response);
    }
}