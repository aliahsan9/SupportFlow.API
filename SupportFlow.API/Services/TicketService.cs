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
    public string GetTicketDetails(int ticketId)
    {
        return ticketId switch
        {
            1001 => "Ticket #1001: Login problem. Customer cannot log in after resetting the password.",
            1002 => "Ticket #1002: Payment problem. Customer reports a duplicate payment.",
            1003 => "Ticket #1003: Refund request. Customer requested a refund for a subscription.",
            1024 => "Ticket #1024: Account locked. Customer is unable to access the account.",
            _ => $"Ticket #{ticketId} was not found."
        };
    }
    public string GetCustomer(int ticketId)
    {
        return ticketId switch
        {
            1001 => "Ticket #1001 was reported by Ali.",
            1002 => "Ticket #1002 was reported by Ahmed.",
            1003 => "Ticket #1003 was reported by Sara.",
            1024 => "Ticket #1024 was reported by Hamza.",
            _ => $"No customer was found for ticket #{ticketId}."
        };
    }
}