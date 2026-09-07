namespace SupportFlow.API.Models;

public sealed class HumanApprovalRequest
{
    public Guid Id { get; set; }

    public string ConversationId { get; set; } = string.Empty;

    public string OriginalRequest { get; set; } = string.Empty;

    public string Reason { get; set; } = string.Empty;

    public string RequestedAction { get; set; } = string.Empty;

    public HumanApprovalStatus Status { get; set; }

    public string? HumanComment { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? RespondedAt { get; set; }
}