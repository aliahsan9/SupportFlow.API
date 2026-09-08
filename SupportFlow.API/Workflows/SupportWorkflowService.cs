using Microsoft.Agents.AI.Workflows;
using SupportFlow.API.Models;

namespace SupportFlow.API.Workflows;

public sealed class SupportWorkflowService
{
    private readonly SupportWorkflow _supportWorkflow;

    public SupportWorkflowService(
        SupportWorkflow supportWorkflow)
    {
        _supportWorkflow = supportWorkflow;
    }

    public async Task<SupportWorkflowResponse> RunAsync(
        SupportWorkflowRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        if (string.IsNullOrWhiteSpace(request.SessionId))
        {
            throw new ArgumentException(
                "SessionId is required.",
                nameof(request));
        }

        if (string.IsNullOrWhiteSpace(request.Message))
        {
            throw new ArgumentException(
                "Message is required.",
                nameof(request));
        }

        Console.WriteLine();
        Console.WriteLine("=================================");
        Console.WriteLine("SUPPORT WORKFLOW SERVICE");
        Console.WriteLine("=================================");

        Console.WriteLine($"Session ID: {request.SessionId}");
        Console.WriteLine($"Message: {request.Message}");

        var workflowInput = new WorkflowRequest
        {
            SessionId = request.SessionId,
            Message = request.Message
        };

        var run = await InProcessExecution.RunAsync(
            _supportWorkflow.Workflow,
            workflowInput,
            request.SessionId,
            cancellationToken);

        string response = string.Empty;

        foreach (var workflowEvent in run.OutgoingEvents)
        {
            Console.WriteLine(
                $"Workflow event: {workflowEvent.GetType().Name}");

            if (workflowEvent is WorkflowOutputEvent outputEvent)
            {
                var output = outputEvent.As<string>();

                if (!string.IsNullOrWhiteSpace(output))
                {
                    response = output;
                }
            }
        }

        if (string.IsNullOrWhiteSpace(response))
        {
            response =
                "The workflow completed without producing a response.";
        }

        Console.WriteLine();
        Console.WriteLine("=================================");
        Console.WriteLine("WORKFLOW COMPLETED");
        Console.WriteLine("=================================");

        Console.WriteLine($"Response: {response}");

        return new SupportWorkflowResponse
        {
            SessionId = request.SessionId,
            Agent = string.Empty,
            Response = response
        };
    }
}