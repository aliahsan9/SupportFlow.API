using Microsoft.Agents.AI;
using Microsoft.Extensions.AI;
using OllamaSharp;
using SupportFlow.API.Services;

namespace SupportFlow.API.Agents;

public class SupportAgent
{
    private readonly AIAgent _agent;

    public SupportAgent(TicketService ticketService)
    {
        var ollamaClient = new OllamaApiClient(
            new Uri("http://localhost:11434"),
            "qwen3:1.7b");

        var ticketStatusTool = AIFunctionFactory.Create(
            ticketService.GetTicketStatus,
            "Get the current status of a support ticket. Use this when the user asks whether a ticket is open, closed, pending, resolved, or in progress.");

        var ticketDetailsTool = AIFunctionFactory.Create(
            ticketService.GetTicketDetails,
            "Get detailed information about a support ticket. Use this when the user asks what happened with a ticket or asks for the ticket's issue/details.");

        var customerTool = AIFunctionFactory.Create(
            ticketService.GetCustomer,
            "Get the customer who reported a support ticket. Use this when the user asks who reported or owns a ticket.");

        _agent = ollamaClient.AsAIAgent(
            instructions: """
                You are SupportFlow Assistant.

                You are a helpful customer support assistant.

                You can answer general questions.

                When the user asks about a support ticket,
                use the appropriate ticket tool.

                Use GetTicketStatus when the user asks
                about the status of a ticket.

                Use GetTicketDetails when the user asks
                about the issue or details of a ticket.

                Use GetCustomer when the user asks
                who reported a ticket.

                Never invent ticket information.

                Always provide a final text response to the user
                after using a tool.
                """,
            name: "SupportAgent",
            tools:
            [
                ticketStatusTool,
                ticketDetailsTool,
                customerTool
            ]);
    }

    public async Task<string> AskAsync(
        string sessionId,
        string message)
    {
        Console.WriteLine("========== SUPPORT REQUEST ==========");
        Console.WriteLine($"SessionId: {sessionId}");
        Console.WriteLine($"Message: {message}");
        Console.WriteLine("=====================================");

        var response = await _agent.RunAsync(message);

        Console.WriteLine("========== AGENT RESPONSE ==========");
        Console.WriteLine($"Response object null: {response == null}");

        if (response != null)
        {
            Console.WriteLine($"Text: '{response.Text}'");
            Console.WriteLine($"Text null: {response.Text == null}");
            Console.WriteLine(
                $"Text empty: {string.IsNullOrWhiteSpace(response.Text)}");
        }

        Console.WriteLine("====================================");

        if (response == null)
        {
            return "The AI agent returned no response.";
        }

        if (string.IsNullOrWhiteSpace(response.Text))
        {
            return "The AI agent returned an empty response.";
        }

        return response.Text;
    }
}
