using CashFlow.Communication.Responses;
using CashFlow.Exception;
using FluentValidation;

namespace CashFlow.App.Validations.Users.Update;

internal class UpdateUserValidator : AbstractValidator<RequestUpdateUser>
{
    public UpdateUserValidator()
    {
        RuleFor(expense => expense.Name).NotEmpty().WithMessage(ResourceErrorMessages.Name_Not_Empty);
        RuleFor(expense => expense.Email)
            .NotEmpty()
            .WithMessage(ResourceErrorMessages.Email_Not_Empty)
            .EmailAddress()
            .When(user => string.IsNullOrWhiteSpace(user.Email) == false, ApplyConditionTo.CurrentValidator)
            .WithMessage(ResourceErrorMessages.Email_Invalid);
    }
}