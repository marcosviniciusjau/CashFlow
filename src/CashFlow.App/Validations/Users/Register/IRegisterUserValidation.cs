using CashFlow.Communication.Responses;

namespace CashFlow.App.Validations.Users.Register;
public interface IRegisterUserValidation
{
    Task<ResponseRegisteredUser> Execute(RequestUser request);
}
