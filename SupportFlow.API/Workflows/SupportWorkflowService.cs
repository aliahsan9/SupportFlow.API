using SupportFlow.API.Agents;

namespace SupportFlow.API.Workflows;

public class SupportWorkflowService
{
    private readonly CoordinatorAgent _coordinator;
    private readonly SupportAgent _supportAgent;
    private readonly BillingAgent _billingAgent;
    private readonly TechnicalAgent _technicalAgent;

    public SupportWorkflowService(
        CoordinatorAgent coordinator,
        SupportAgent supportAgent,
        BillingAgent billingAgent,
        TechnicalAgent technicalAgent)
    {
        _coordinator = coordinator;
        _supportAgent = supportAgent;
        _billingAgent = billingAgent;
        _technicalAgent = technicalAgent;
    }

    public async Task<SupportWorkflowResponse> RunAsync(
        SupportWorkflowRequest request)
    {
        var agent =
            await _coordinator.DetermineAgentAsync(request.Message);

        string response;

        switch (agent)
        {
            case "SUPPORT":
                response =
                    await _supportAgent.AskAsync(
                        request.SessionId,
                        request.Message);
                break;

            case "BILLING":
                response =
                    await _billingAgent.AskAsync(
                        request.Message);
                break;

            case "TECHNICAL":
                response =
                    await _technicalAgent.AskAsync(
                        request.Message);
                break;

            default:
                response =
                    "I couldn't determine which specialist should handle your request.";
                break;
        }

        return new SupportWorkflowResponse
        {
            SessionId = request.SessionId,
            Agent = agent,
            Response = response
        };
    }
}
