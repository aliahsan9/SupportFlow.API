using Microsoft.Agents.AI.Workflows;

namespace SupportFlow.API.Executors;

public sealed class ResponseExecutor : Executor<string>
{
    public ResponseExecutor()
        : base("ResponseExecutor")
    {
    }

    public override ValueTask HandleAsync(
        string response,
        IWorkflowContext context,
        CancellationToken cancellationToken = default)
    {
        Console.WriteLine();
        Console.WriteLine("=================================");
        Console.WriteLine("RESPONSE EXECUTOR");
        Console.WriteLine("=================================");

        Console.WriteLine("Final response:");
        Console.WriteLine(response);

        return ValueTask.CompletedTask;
    }
}