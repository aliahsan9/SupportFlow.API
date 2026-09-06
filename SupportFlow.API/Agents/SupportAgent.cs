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

            When a user asks about a support ticket,
            use the GetTicketStatus tool to retrieve
            the ticket information.

            Never invent ticket information.
            """,
            name: "SupportAgent",
            tools: [ticketTool]);
    }
}