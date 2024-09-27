using CashFlow.Communication.Requests;
using FluentValidation;

namespace CashFlow.App.Validations.Users.ChangePassword;
public class ChangePasswordValidator : AbstractValidator<RequestChangePassword>
{
    public ChangePasswordValidator()
    {
        RuleFor(x => x.NewPassword).SetValidator(new PasswordValidator<RequestChangePassword>());
    }
}
