using CashFlow.Communication.Responses;

namespace CashFlow.App.Validations.Users.Update;
public interface IUpdateProfileValidation
{
    public Task Execute(RequestUpdateUser request);
}
