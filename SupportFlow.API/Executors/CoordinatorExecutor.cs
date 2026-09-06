using Microsoft.Agents.AI.Workflows;
using SupportFlow.API.Agents;

namespace SupportFlow.API.Executors;

public sealed class CoordinatorExecutor : Executor
{
    private readonly CoordinatorAgent _coordinatorAgent;

    public CoordinatorExecutor(
        CoordinatorAgent coordinatorAgent)
        : base("CoordinatorExecutor")
    {
        _coordinatorAgent = coordinatorAgent;
    }

    public async ValueTask HandleAsync(
        string message,
        IWorkflowContext context)
    {
        Console.WriteLine();
        Console.WriteLine("=================================");
        Console.WriteLine("COORDINATOR EXECUTOR");
        Console.WriteLine("=================================");

        var agent =
            await _coordinatorAgent.DetermineAgentAsync(message);

        Console.WriteLine($"Selected agent: {agent}");

        await context.SendMessageAsync(
            new AgentSelection
            {
                Agent = agent,
                Message = message
            });
    }
}