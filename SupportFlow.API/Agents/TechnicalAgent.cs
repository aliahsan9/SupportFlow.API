using Microsoft.Agents.AI;
using Microsoft.Extensions.AI;
using OllamaSharp;

namespace SupportFlow.API.Agents;

public class TechnicalAgent
{
    private readonly AIAgent _agent;

    public TechnicalAgent()
    {
        var ollamaClient = new OllamaApiClient(
            new Uri("http://localhost:11434"),
            "qwen3:1.7b");

        _agent = ollamaClient.AsAIAgent(
            instructions: """
            You are the Technical Support Agent for SupportFlow.

            Your responsibility is to handle technical support questions.

            You can help with:
            - Login problems
            - Application errors
            - Configuration problems
            - Connection problems
            - Technical troubleshooting

            Provide clear troubleshooting steps.

            Do not invent facts about the user's system.

            Stay focused on technical support.

            If the request is clearly about billing or ticket management,
            explain that another specialist should handle it.
            """);
    }

    public async Task<string> AskAsync(string message)
    {
        var response = await _agent.RunAsync(message);

        return response.ToString();
    }
}