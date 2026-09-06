using Microsoft.Agents.AI.Workflows;

namespace SupportFlow.API.Executors;

public sealed class ResponseExecutor : Executor<AgentSelection, string>
{
    public ResponseExecutor()
        : base("ResponseExecutor")
    {
    }

    public override async ValueTask<string> HandleAsync(
        AgentSelection selection,
        IWorkflowContext context,
        CancellationToken cancellationToken = default)
    {
        Console.WriteLine();
        Console.WriteLine("=================================");
        Console.WriteLine("RESPONSE EXECUTOR");
        Console.WriteLine("=================================");

        Console.WriteLine(
            $"Agent selected: {selection.Agent}");

        return $"Agent selected: {selection.Agent}";
    }
}