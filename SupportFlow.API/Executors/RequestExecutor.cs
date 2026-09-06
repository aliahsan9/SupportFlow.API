using Microsoft.Agents.AI.Workflows;

namespace SupportFlow.API.Executors;

public sealed class RequestExecutor : Executor<string, string>
{
    public RequestExecutor()
        : base("RequestExecutor")
    {
    }

    public override async ValueTask<string> HandleAsync(
        string message,
        IWorkflowContext context,
        CancellationToken cancellationToken = default)
    {
        Console.WriteLine();
        Console.WriteLine("=================================");
        Console.WriteLine("REQUEST EXECUTOR");
        Console.WriteLine("=================================");
        Console.WriteLine($"Incoming message: {message}");

        return message;
    }
}