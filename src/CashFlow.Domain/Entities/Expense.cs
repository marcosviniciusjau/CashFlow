namespace CashFlow.Domain.Entities;
using CashFlow.Domain.Entities.Enums;
public class Expense
{
    public long Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }

    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public PaymentTypes PaymentType { get; set; }
    public long UserId { get; set; }
    public User User { get; set; } = default!;
}
