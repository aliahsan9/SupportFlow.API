using Microsoft.Agents.AI.Workflows;
using SupportFlow.API.Agents;
using SupportFlow.API.Models;

namespace SupportFlow.API.Executors;

public sealed class CoordinatorExecutor
    : Executor<WorkflowRequest, AgentSelection>
{
    private readonly CoordinatorAgent _coordinatorAgent;

    public CoordinatorExecutor(
        CoordinatorAgent coordinatorAgent)
        : base("CoordinatorExecutor")
    {
        _coordinatorAgent = coordinatorAgent;
    }

    public override async ValueTask<AgentSelection> HandleAsync(
        WorkflowRequest request,
        IWorkflowContext context,
        CancellationToken cancellationToken = default)
    {
        Console.WriteLine();
        Console.WriteLine("=================================");
        Console.WriteLine("COORDINATOR EXECUTOR");
        Console.WriteLine("=================================");

        Console.WriteLine($"Session ID: {request.SessionId}");
        Console.WriteLine($"Incoming message: {request.Message}");

        cancellationToken.ThrowIfCancellationRequested();

        var agent =
            await _coordinatorAgent.DetermineAgentAsync(
                request.Message);

        Console.WriteLine($"Selected agent: {agent}");

        return new AgentSelection
        {
            Agent = agent,
            Message = request.Message,
            SessionId = request.SessionId
        };
    }
}