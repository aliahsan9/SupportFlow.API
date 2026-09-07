using Microsoft.Extensions.Logging;

namespace SupportFlow.API.Events;

public sealed class WorkflowEventLogger
{
    private readonly ILogger<WorkflowEventLogger> _logger;

    public WorkflowEventLogger(
        ILogger<WorkflowEventLogger> logger)
    {
        _logger = logger;
    }

    public void Publish(WorkflowEvent workflowEvent)
    {
        _logger.LogInformation(
            "Workflow Event | Type: {Type} | WorkflowId: {WorkflowId} | Executor: {ExecutorName} | Message: {Message} | Timestamp: {TimestampUtc}",
            workflowEvent.Type,
            workflowEvent.WorkflowId,
            workflowEvent.ExecutorName,
            workflowEvent.Message,
            workflowEvent.TimestampUtc);
    }
}