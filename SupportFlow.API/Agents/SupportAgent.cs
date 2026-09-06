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

        var ticketTool = AIFunctionFactory.Create(
            ticketService.GetTicketStatus,
            "Get the status of a customer support ticket by ticket ID.");

        _agent = ollamaClient.AsAIAgent(
            instructions: """
            You are SupportFlow Assistant.

            You are a helpful customer support assistant.

            You can answer general questions.

            When the user asks about a support ticket,
            you MUST use the GetTicketStatus tool.

            Never invent ticket information.

            If the user provides a ticket number,
            extract the numeric ticket ID and call the tool.

            Always provide a text response to the user.
            """,
            name: "SupportAgent",
            tools: [ticketTool]);
    }

    public async Task<string> AskAsync(
        string sessionId,
        string message)
    {
        var response = await _agent.RunAsync(message);

        Console.WriteLine("========== AGENT RESPONSE ==========");
        Console.WriteLine($"Text: '{response.Text}'");
        Console.WriteLine("====================================");

        return response.Text;
    }
}

