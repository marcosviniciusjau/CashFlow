using CashFlow.Communication.Requests;
using CashFlow.Exception;
using FluentValidation;

namespace CashFlow.App.Validations.Expenses;

public class ExpenseValidator : AbstractValidator<RequestExpenses>
{
    public ExpenseValidator()
    {
        RuleFor(expense => expense.Title).NotEmpty().WithMessage(ResourceErrorMessages.Title_Required);
        RuleFor(expense => expense.Amount).GreaterThan(0).WithMessage(ResourceErrorMessages.Amount_Greather_Than_0);
        RuleFor(expense => expense.Date).LessThan(DateTime.UtcNow).WithMessage(ResourceErrorMessages.Expenses_Not_In_Future);
        RuleFor(expense => expense.PaymentType).IsInEnum().WithMessage(ResourceErrorMessages.Payment_Invalid);
    }
}
