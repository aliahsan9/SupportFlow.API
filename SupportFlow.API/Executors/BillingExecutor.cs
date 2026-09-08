using Microsoft.Agents.AI.Workflows;
using SupportFlow.API.Agents;

namespace SupportFlow.API.Executors;

public sealed class BillingExecutor
    : Executor<AgentSelection, string>
{
    private readonly BillingAgent _billingAgent;

    public BillingExecutor(
        BillingAgent billingAgent)
        : base("BillingExecutor")
    {
        _billingAgent = billingAgent;
    }

    public override async ValueTask<string> HandleAsync(
        AgentSelection selection,
        IWorkflowContext context,
        CancellationToken cancellationToken = default)
    {
        Console.WriteLine();
        Console.WriteLine("=================================");
        Console.WriteLine("BILLING EXECUTOR");
        Console.WriteLine("=================================");

        Console.WriteLine($"Agent: {selection.Agent}");
        Console.WriteLine($"Session ID: {selection.SessionId}");
        Console.WriteLine($"Message: {selection.Message}");

        cancellationToken.ThrowIfCancellationRequested();

        var response = await _billingAgent.AskAsync(
            selection.Message);

        Console.WriteLine("Billing response generated.");

        return response;
    }
}