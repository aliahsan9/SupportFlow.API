using Microsoft.Agents.AI.Workflows;
using SupportFlow.API.Agents;

namespace SupportFlow.API.Executors;

public sealed class SupportExecutor : Executor<AgentSelection, string>
{
    private readonly SupportAgent _supportAgent;

    public SupportExecutor(
        SupportAgent supportAgent)
        : base("SupportExecutor")
    {
        _supportAgent = supportAgent;
    }

    public override async ValueTask<string> HandleAsync(
        AgentSelection selection,
        IWorkflowContext context,
        CancellationToken cancellationToken = default)
    {
        Console.WriteLine();
        Console.WriteLine("=================================");
        Console.WriteLine("SUPPORT EXECUTOR");
        Console.WriteLine("=================================");

        Console.WriteLine($"Agent: {selection.Agent}");
        Console.WriteLine($"Message: {selection.Message}");

        var response = await _supportAgent.AskAsync(
            selection.SessionId,
            selection.Message);

        Console.WriteLine("Support response generated.");

        return response;
    }
}