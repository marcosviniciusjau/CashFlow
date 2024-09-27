using CashFlow.Communication.Requests;

namespace CashFlow.App.Validations.Users.ChangePassword;
public interface IChangePasswordValidation
{
    Task Execute(RequestChangePassword request);
}
