using Microsoft.Agents.AI.Workflows;

namespace SupportFlow.API.Executors;

public sealed class RequestExecutor : Executor
{
    public RequestExecutor()
        : base("RequestExecutor")
    {
    }

    public async ValueTask HandleAsync(
        string message,
        IWorkflowContext context)
    {
        Console.WriteLine();
        Console.WriteLine("=================================");
        Console.WriteLine("REQUEST EXECUTOR");
        Console.WriteLine("=================================");
        Console.WriteLine($"Incoming message: {message}");

        await context.SendMessageAsync(message);
    }
}