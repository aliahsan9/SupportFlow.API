namespace SupportFlow.API.Services;

public class TicketService
{
    public string GetTicketStatus(int ticketId)
    {
        return ticketId switch
        {
            1001 => "Ticket #1001: Login problem. Status: In Progress.",
            1002 => "Ticket #1002: Payment problem. Status: Resolved.",
            1003 => "Ticket #1003: Refund request. Status: Pending.",
            1024 => "Ticket #1024: Account locked. Status: In Progress.",
            _ => $"Ticket #{ticketId} was not found."
        };
    }
}