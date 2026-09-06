using Microsoft.Agents.AI.Workflows;

namespace SupportFlow.API.Executors;

public sealed class ResponseExecutor : Executor
{
    public ResponseExecutor()
        : base("ResponseExecutor")
    {
    }

    public async ValueTask HandleAsync(
        AgentSelection selection,
        IWorkflowContext context)
    {
        Console.WriteLine();
        Console.WriteLine("=================================");
        Console.WriteLine("RESPONSE EXECUTOR");
        Console.WriteLine("=================================");

        Console.WriteLine(
            $"Agent selected: {selection.Agent}");

        await context.SendMessageAsync(
            $"Agent selected: {selection.Agent}");
    }
}