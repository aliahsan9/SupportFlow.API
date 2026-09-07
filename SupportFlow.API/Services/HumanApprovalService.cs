using SupportFlow.API.Models;

namespace SupportFlow.API.Services;

public sealed class HumanApprovalService
{
    private readonly Dictionary<Guid, HumanApprovalRequest> _requests = new();

    public HumanApprovalRequest CreateRequest(
        string conversationId,
        string originalRequest,
        string reason,
        string requestedAction)
    {
        var request = new HumanApprovalRequest
        {
            Id = Guid.NewGuid(),
            ConversationId = conversationId,
            OriginalRequest = originalRequest,
            Reason = reason,
            RequestedAction = requestedAction,
            Status = HumanApprovalStatus.Pending,
            CreatedAt = DateTime.UtcNow
        };

        _requests[request.Id] = request;

        return request;
    }

    public HumanApprovalRequest? GetRequest(Guid id)
    {
        return _requests.TryGetValue(id, out var request)
            ? request
            : null;
    }

    public bool Approve(Guid id, string? comment)
    {
        if (!_requests.TryGetValue(id, out var request))
        {
            return false;
        }

        if (request.Status != HumanApprovalStatus.Pending)
        {
            return false;
        }

        request.Status = HumanApprovalStatus.Approved;
        request.HumanComment = comment;
        request.RespondedAt = DateTime.UtcNow;

        return true;
    }

    public bool Reject(Guid id, string? comment)
    {
        if (!_requests.TryGetValue(id, out var request))
        {
            return false;
        }

        if (request.Status != HumanApprovalStatus.Pending)
        {
            return false;
        }

        request.Status = HumanApprovalStatus.Rejected;
        request.HumanComment = comment;
        request.RespondedAt = DateTime.UtcNow;

        return true;
    }

    public IReadOnlyCollection<HumanApprovalRequest> GetPendingRequests()
    {
        return _requests.Values
            .Where(x => x.Status == HumanApprovalStatus.Pending)
            .ToList();
    }
}