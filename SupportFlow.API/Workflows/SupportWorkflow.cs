using Microsoft.Agents.AI.Workflows;
using SupportFlow.API.Executors;

namespace SupportFlow.API.Workflows;

public sealed class SupportWorkflow
{
    private readonly Workflow _workflow;

    public SupportWorkflow(
        RequestExecutor requestExecutor,
        CoordinatorExecutor coordinatorExecutor,
        SupportExecutor supportExecutor,
        BillingExecutor billingExecutor,
        TechnicalExecutor technicalExecutor,
        ResponseExecutor responseExecutor)
    {
        var builder =
            new WorkflowBuilder(requestExecutor);

        // ==========================================
        // REQUEST → COORDINATOR
        // ==========================================

        builder.AddEdge(
            requestExecutor,
            coordinatorExecutor);

        // ==========================================
        // COORDINATOR → SUPPORT
        // ==========================================

        builder.AddEdge<AgentSelection>(
            coordinatorExecutor,
            supportExecutor,
            selection =>
                selection.Agent == "SUPPORT");

        // ==========================================
        // COORDINATOR → BILLING
        // ==========================================

        builder.AddEdge<AgentSelection>(
            coordinatorExecutor,
            billingExecutor,
            selection =>
                selection.Agent == "BILLING");

        // ==========================================
        // COORDINATOR → TECHNICAL
        // ==========================================

        builder.AddEdge<AgentSelection>(
            coordinatorExecutor,
            technicalExecutor,
            selection =>
                selection.Agent == "TECHNICAL");

        // ==========================================
        // SPECIALIST → RESPONSE
        // ==========================================

        builder.AddEdge(
            supportExecutor,
            responseExecutor);

        builder.AddEdge(
            billingExecutor,
            responseExecutor);

        builder.AddEdge(
            technicalExecutor,
            responseExecutor);

        _workflow = builder.Build();
    }

    public Workflow Workflow => _workflow;
}