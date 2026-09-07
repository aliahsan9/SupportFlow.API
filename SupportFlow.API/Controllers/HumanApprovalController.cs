using Microsoft.AspNetCore.Mvc;
using SupportFlow.API.Models;
using SupportFlow.API.Services;

namespace SupportFlow.API.Controllers;

[ApiController]
[Route("api/support/approvals")]
public sealed class HumanApprovalController : ControllerBase
{
    private readonly HumanApprovalService _approvalService;

    public HumanApprovalController(
        HumanApprovalService approvalService)
    {
        _approvalService = approvalService;
    }

    [HttpGet]
    public IActionResult GetPendingApprovals()
    {
        var requests = _approvalService.GetPendingRequests();

        return Ok(requests);
    }

    [HttpGet("{id:guid}")]
    public IActionResult GetApproval(Guid id)
    {
        var request = _approvalService.GetRequest(id);

        if (request is null)
        {
            return NotFound();
        }

        return Ok(request);
    }

    [HttpPost("{id:guid}/approve")]
    public IActionResult Approve(
        Guid id,
        [FromBody] ApprovalResponse request)
    {
        var success = _approvalService.Approve(
            id,
            request.Comment);

        if (!success)
        {
            return BadRequest(
                "Approval request was not found or has already been processed.");
        }

        return Ok(new
        {
            message = "Approval granted.",
            requestId = id
        });
    }

    [HttpPost("{id:guid}/reject")]
    public IActionResult Reject(
        Guid id,
        [FromBody] ApprovalResponse request)
    {
        var success = _approvalService.Reject(
            id,
            request.Comment);

        if (!success)
        {
            return BadRequest(
                "Approval request was not found or has already been processed.");
        }

        return Ok(new
        {
            message = "Approval rejected.",
            requestId = id
        });
    }
}

public sealed class ApprovalResponse
{
    public string? Comment { get; set; }
}