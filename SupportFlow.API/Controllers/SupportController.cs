using Microsoft.AspNetCore.Mvc;
using SupportFlow.API.Models;
using SupportFlow.API.Workflows;

namespace SupportFlow.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SupportController : ControllerBase
{
    private readonly SupportWorkflowService _workflow;

    public SupportController(
        SupportWorkflowService workflow)
    {
        _workflow = workflow;
    }

    [HttpPost("ask")]
    public async Task<IActionResult> Ask(
        [FromBody] ChatRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Message))
        {
            return BadRequest("Message cannot be empty.");
        }

        var workflowRequest = new SupportWorkflowRequest
        {
            SessionId = request.SessionId,
            Message = request.Message
        };

        var result =
            await _workflow.RunAsync(workflowRequest);

        return Ok(result);
    }
}