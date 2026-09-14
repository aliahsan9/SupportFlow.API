using Microsoft.AspNetCore.Mvc;
using SupportFlow.API.Models;
using SupportFlow.API.Services;
using SupportFlow.API.Workflows;

namespace SupportFlow.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SupportController : ControllerBase
{
    private readonly SupportWorkflowService _supportWorkflowService;

    public SupportController(
        SupportWorkflowService supportWorkflowService)
    {
        _supportWorkflowService = supportWorkflowService;
    }

    [HttpPost]
    public async Task<ActionResult<SupportWorkflowResponse>> Process(
        [FromBody] SupportWorkflowRequest request,
        CancellationToken cancellationToken)
    {
        var response = await _supportWorkflowService.RunAsync(
            request,
            cancellationToken);

        return Ok(response);
    }
}
