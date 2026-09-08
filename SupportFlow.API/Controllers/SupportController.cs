using Microsoft.AspNetCore.Mvc;
using SupportFlow.API.Models;
using SupportFlow.API.Workflows;

namespace SupportFlow.API.Controllers;

[ApiController]
[Route("api/support")]
public sealed class SupportController : ControllerBase
{
    private readonly SupportWorkflowService _workflow;

    public SupportController(
        SupportWorkflowService workflow)
    {
        _workflow = workflow;
    }

    [HttpPost("chat")]
    public async Task<ActionResult<SupportChatResponse>> Chat(
        [FromBody] SupportChatRequest request,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Message))
        {
            return BadRequest("Message is required.");
        }

        if (string.IsNullOrWhiteSpace(request.ConversationId))
        {
            return BadRequest("ConversationId is required.");
        }

        var result = await _workflow.ProcessAsync(
            request,
            cancellationToken);

        return Ok(result);
    }
}