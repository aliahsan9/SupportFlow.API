using Microsoft.Agents.AI;
namespace SupportFlow.API.Middleware;

public static class AgentLoggingMiddleware
{
    public static async Task InvokeAsync(
        AgentRunContext context,
        Func<Task> next)
    {
        Console.WriteLine("=================================");
        Console.WriteLine("AGENT MIDDLEWARE");
        Console.WriteLine("=================================");

        // Log incoming request messages
        foreach (var message in context.RequestMessages)
        {
            Console.WriteLine($"Request: {message}");
        }

        Console.WriteLine("=================================");

        // Continue execution
        await next();

        Console.WriteLine("=================================");
        Console.WriteLine("AGENT MIDDLEWARE - RESPONSE");
        Console.WriteLine("Agent execution completed.");
        Console.WriteLine("=================================");
    }
}