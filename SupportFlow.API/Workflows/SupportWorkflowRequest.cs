namespace SupportFlow.API.Workflows;

public class SupportWorkflowRequest
{
    public string SessionId { get; set; } = string.Empty;

    public string Message { get; set; } = string.Empty;
}