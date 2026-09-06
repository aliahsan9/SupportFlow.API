using Microsoft.Agents.AI;
using Microsoft.Extensions.AI;
using OllamaSharp;

namespace SupportFlow.API.Agents;

public class BillingAgent
{
    private readonly AIAgent _agent;

    public BillingAgent()
    {
        var ollamaClient = new OllamaApiClient(
            new Uri("http://localhost:11434"),
            "qwen3:1.7b");

        _agent = ollamaClient.AsAIAgent(
            instructions: """
            You are the Billing Support Agent for SupportFlow.

            Your responsibility is to handle billing-related questions.

            You can help with:
            - Payments
            - Invoices
            - Refunds
            - Duplicate charges
            - Billing problems

            Stay focused on billing.

            Do not invent payment information, invoice information,
            transaction information, or refund status.

            If the user asks about something outside billing,
            explain that another specialist should handle the request.
            """);
    }

    public async Task<string> AskAsync(string message)
    {
        var response = await _agent.RunAsync(message);

        return response.ToString();
    }
}