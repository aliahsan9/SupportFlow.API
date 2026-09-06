using Microsoft.Agents.AI.Workflows;
using SupportFlow.API.Executors;

namespace SupportFlow.API.Workflows;

public sealed class SupportWorkflow
{
    private readonly Workflow _workflow;

    public SupportWorkflow(
        RequestExecutor requestExecutor,
        CoordinatorExecutor coordinatorExecutor,
        ResponseExecutor responseExecutor)
    {
        var builder =
            new WorkflowBuilder(requestExecutor);

        builder.AddEdge(
            requestExecutor,
            coordinatorExecutor);

        builder.AddEdge(
            coordinatorExecutor,
            responseExecutor);

        _workflow = builder.Build();
    }

    public Workflow Workflow => _workflow;
}