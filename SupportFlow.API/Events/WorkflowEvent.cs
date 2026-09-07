namespace SupportFlow.API.Events;

public sealed class WorkflowEvent
{
    public WorkflowEventType Type { get; init; }

    public string WorkflowId { get; init; } = string.Empty;

    public string? ExecutorName { get; init; }

    public string? Message { get; init; }

    public DateTime TimestampUtc { get; init; } = DateTime.UtcNow;
}