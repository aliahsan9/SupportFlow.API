namespace SupportFlow.API.Models;

public sealed class SupportChatRequest
{
    public string ConversationId { get; set; } = string.Empty;

    public string Message { get; set; } = string.Empty;
}