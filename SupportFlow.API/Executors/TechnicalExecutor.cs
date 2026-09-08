using Microsoft.Agents.AI.Workflows;
using SupportFlow.API.Agents;

namespace SupportFlow.API.Executors;

public sealed class TechnicalExecutor
    : Executor<AgentSelection, string>
{
    private readonly TechnicalAgent _technicalAgent;

    public TechnicalExecutor(
        TechnicalAgent technicalAgent)
        : base("TechnicalExecutor")
    {
        _technicalAgent = technicalAgent;
    }

    public override async ValueTask<string> HandleAsync(
        AgentSelection selection,
        IWorkflowContext context,
        CancellationToken cancellationToken = default)
    {
        Console.WriteLine();
        Console.WriteLine("=================================");
        Console.WriteLine("TECHNICAL EXECUTOR");
        Console.WriteLine("=================================");

        Console.WriteLine($"Agent: {selection.Agent}");
        Console.WriteLine($"Session ID: {selection.SessionId}");
        Console.WriteLine($"Message: {selection.Message}");

        cancellationToken.ThrowIfCancellationRequested();

        var response = await _technicalAgent.AskAsync(
            selection.Message);

        Console.WriteLine("Technical response generated.");

        return response;
    }
}