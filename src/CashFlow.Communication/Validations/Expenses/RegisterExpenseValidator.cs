using CashFlow.Communication.Requests;
using FluentValidation;

namespace CashFlow.Communication.Validations.Expenses;

public class RegisterExpenseValidator: AbstractValidator<RequestExpenses>
{
    public RegisterExpenseValidator()
    {
        RuleFor(expense => expense.Title).NotEmpty().WithMessage("Title is required");
        RuleFor(expense => expense.Amount).GreaterThan(0).WithMessage("Amount must be greater than 0");
        RuleFor(expense => expense.Date).LessThan(DateTime.UtcNow).WithMessage("Date cannot be in the future");
        RuleFor(expense => expense.PaymentType).IsInEnum().WithMessage("Payment type not valid");
    }
}
