using SupportFlow.API.Agents;

namespace SupportFlow.API.Services;

public class MultiAgentService
{
    private readonly CoordinatorAgent _coordinator;
    private readonly SupportAgent _supportAgent;
    private readonly BillingAgent _billingAgent;
    private readonly TechnicalAgent _technicalAgent;

    public MultiAgentService(
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

    public async Task<string> AskAsync(string message)
    {
        var agentType =
            await _coordinator.DetermineAgentAsync(message);

        return agentType switch
        {
            "SUPPORT" =>
                await _supportAgent.AskAsync(message),

            "BILLING" =>
                await _billingAgent.AskAsync(message),

            "TECHNICAL" =>
                await _technicalAgent.AskAsync(message),

            _ =>
                "I couldn't determine which specialist should handle your request."
        };
    }
}