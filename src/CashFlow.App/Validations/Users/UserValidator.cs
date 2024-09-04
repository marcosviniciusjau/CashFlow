using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using CashFlow.Exception;
using FluentValidation;

namespace CashFlow.App.Validations.Users;
public class UserValidator : AbstractValidator<RequestUser>
{
    public UserValidator()
    {
        RuleFor(expense => expense.Name).NotEmpty().WithMessage(ResourceErrorMessages.Name_Not_Empty);
        RuleFor(expense => expense.Email)
            .NotEmpty()
            .WithMessage(ResourceErrorMessages.Email_Not_Empty)
            .EmailAddress()
            .WithMessage(ResourceErrorMessages.Email_Not_Empty)
            ;
        RuleFor(user => user.Password).SetValidator(new PasswordValidator<RequestUser>());
    
    }
}
