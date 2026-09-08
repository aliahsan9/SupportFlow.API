namespace SupportFlow.API.Models;

public sealed class SupportChatResponse
{
    public string ConversationId { get; set; } = string.Empty;

    public string Status { get; set; } = string.Empty;

    public string Message { get; set; } = string.Empty;

    public string? Agent { get; set; }

    public Guid? ApprovalRequestId { get; set; }
}