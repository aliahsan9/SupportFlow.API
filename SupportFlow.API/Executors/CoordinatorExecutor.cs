using Microsoft.Agents.AI.Workflows;
using SupportFlow.API.Agents;

namespace SupportFlow.API.Executors;

public sealed class CoordinatorExecutor : Executor<string, AgentSelection>
{
    private readonly CoordinatorAgent _coordinatorAgent;

    public CoordinatorExecutor(
        CoordinatorAgent coordinatorAgent)
        : base("CoordinatorExecutor")
    {
        _coordinatorAgent = coordinatorAgent;
    }

    public override async ValueTask<AgentSelection> HandleAsync(
        string message,
        IWorkflowContext context,
        CancellationToken cancellationToken = default)
    {
        Console.WriteLine();
        Console.WriteLine("=================================");
        Console.WriteLine("COORDINATOR EXECUTOR");
        Console.WriteLine("=================================");

        var agent =
            await _coordinatorAgent.DetermineAgentAsync(message);

        Console.WriteLine($"Selected agent: {agent}");

        return new AgentSelection
        {
            Agent = agent,
            Message = message
        };
    }
}