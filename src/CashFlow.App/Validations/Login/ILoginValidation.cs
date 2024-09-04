
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;

namespace CashFlow.App.Validations.Login;
public interface ILoginValidation
{
    Task<ResponseRegisteredUser> Execute(RequestLogin request);
}
