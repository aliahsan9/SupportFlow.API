namespace SupportFlow.API.Models;

public sealed class AgentMemory
{
    public Guid Id { get; set; }

    public string ConversationId { get; set; } = string.Empty;

    public string Key { get; set; } = string.Empty;

    public string Value { get; set; } = string.Empty;

    public string? Category { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}