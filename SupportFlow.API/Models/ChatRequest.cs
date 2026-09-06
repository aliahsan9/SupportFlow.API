namespace SupportFlow.API.Models;

public class ChatRequest
{
    public string SessionId { get; set; } = string.Empty;

    public string Message { get; set; } = string.Empty;
} 