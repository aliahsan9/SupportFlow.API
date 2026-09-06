using Microsoft.Agents.AI;
using Microsoft.Extensions.AI;
using OllamaSharp;

namespace SupportFlow.API.Agents;

public class SupportAgent
{
    private readonly AIAgent _agent;

    public SupportAgent() 
    {
        var ollamaClient = new OllamaApiClient(
            new Uri("http://localhost:11434"),
            "qwen3:1.7b");

        _agent = ollamaClient.AsAIAgent(
            instructions: """
                You are SupportFlow Assistant.

                You are a helpful customer support assistant.
                Answer questions clearly and professionally.
                Keep your answers concise.
                """,
            name: "SupportAgent");
    }

    public async Task<string> AskAsync(string message)
    {
        var response = await _agent.RunAsync(message);

        return response.ToString();
    }
}