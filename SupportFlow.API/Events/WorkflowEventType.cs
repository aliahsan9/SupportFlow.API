namespace SupportFlow.API.Events;

public enum WorkflowEventType
{
    WorkflowStarted,

    WorkflowCompleted,

    WorkflowFailed,

    ExecutorStarted,

    ExecutorCompleted,

    ExecutorFailed,

    RoutingDecision
}