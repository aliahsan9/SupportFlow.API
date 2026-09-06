using Microsoft.Agents.AI;
using Microsoft.Extensions.AI;
using OllamaSharp;

namespace SupportFlow.API.Agents;

public class CoordinatorAgent
{
    private readonly AIAgent _agent;

    public CoordinatorAgent()
    {
        var ollamaClient = new OllamaApiClient(
            new Uri("http://localhost:11434"),
            "qwen3:1.7b");

        _agent = ollamaClient.AsAIAgent(
            instructions: """
            You are the SupportFlow Coordinator Agent.

            Your job is to classify the user's request.

            Available specialists:

            SUPPORT
            - Tickets
            - Ticket status
            - General customer support

            BILLING
            - Payments
            - Invoices
            - Refunds
            - Duplicate charges
            - Billing problems

            TECHNICAL
            - Login problems
            - Application errors
            - Configuration problems
            - Connection problems
            - Technical troubleshooting

            Respond with ONLY one of these values:

            SUPPORT
            BILLING
            TECHNICAL

            Do not provide an explanation.
            Do not answer the user's question.
            Only return the specialist name.
            """);
    }

    public async Task<string> DetermineAgentAsync(string message)
    {
        var response = await _agent.RunAsync(message);

        return response.ToString()
            .Trim()
            .ToUpperInvariant();
    }
}