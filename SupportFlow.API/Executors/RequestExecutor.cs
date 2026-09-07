using Microsoft.Agents.AI.Workflows;
using SupportFlow.API.Models;

namespace SupportFlow.API.Executors;

public sealed class RequestExecutor
    : Executor<WorkflowRequest, WorkflowRequest>
{
    public RequestExecutor()
        : base("RequestExecutor")
    {
    }

    public override ValueTask<WorkflowRequest> HandleAsync(
        WorkflowRequest request,
        IWorkflowContext context,
        CancellationToken cancellationToken = default)
    {
        Console.WriteLine();
        Console.WriteLine("=================================");
        Console.WriteLine("REQUEST EXECUTOR");
        Console.WriteLine("=================================");

        Console.WriteLine($"Session ID: {request.SessionId}");
        Console.WriteLine($"Message: {request.Message}");

        return ValueTask.FromResult(request);
    }
}