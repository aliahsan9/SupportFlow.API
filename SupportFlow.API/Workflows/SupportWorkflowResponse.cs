namespace SupportFlow.API.Workflows;

public class SupportWorkflowResponse
{
    public string SessionId { get; set; } = string.Empty;

    public string Agent { get; set; } = string.Empty;

    public string Response { get; set; } = string.Empty;
}