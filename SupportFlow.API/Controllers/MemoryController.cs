using Microsoft.AspNetCore.Mvc;
using SupportFlow.API.Services;

namespace SupportFlow.API.Controllers;

[ApiController]
[Route("api/memory")]
public sealed class MemoryController : ControllerBase
{
    private readonly AgentMemoryService _memoryService;

    public MemoryController(
        AgentMemoryService memoryService)
    {
        _memoryService = memoryService;
    }

    [HttpGet("{conversationId}")]
    public async Task<IActionResult> GetMemories(
        string conversationId,
        CancellationToken cancellationToken)
    {
        var memories = await _memoryService.GetMemoriesAsync(
            conversationId,
            cancellationToken);

        return Ok(memories);
    }

    [HttpPost]
    public async Task<IActionResult> SaveMemory(
        [FromBody] SaveMemoryRequest request,
        CancellationToken cancellationToken)
    {
        var memory = await _memoryService.SaveMemoryAsync(
            request.ConversationId,
            request.Key,
            request.Value,
            request.Category,
            cancellationToken);

        return Ok(memory);
    }

    [HttpDelete("{conversationId}/{key}")]
    public async Task<IActionResult> DeleteMemory(
        string conversationId,
        string key,
        CancellationToken cancellationToken)
    {
        var deleted = await _memoryService.DeleteMemoryAsync(
            conversationId,
            key,
            cancellationToken);

        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }
}

public sealed class SaveMemoryRequest
{
    public string ConversationId { get; set; } = string.Empty;

    public string Key { get; set; } = string.Empty;

    public string Value { get; set; } = string.Empty;

    public string? Category { get; set; }
}