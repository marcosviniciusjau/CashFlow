using CashFlow.Exception.ExceptionBase;
using System.Net;

namespace CashFlow.Exception.ExceptionsBase;
public class InvalidLogin : CashFlowException
{
    public InvalidLogin() : base(ResourceErrorMessages.Invalid_Login)
    {
    }

    public override int StatusCode => (int)HttpStatusCode.Unauthorized;

    public override List<string> GetErrors()
    {
        return [Message];
    }
}