using System.Net;

namespace CashFlow.Exception.ExceptionBase
{
    public class InvalidLogin: CashFlowException
    {
        public InvalidLogin() : base(ResourceErrorMessages.Invalid_Login)
        {
        }

        public  int StatusCode => (int)HttpStatusCode.Unauthorized;

        public  List<string> GetErrors()
        {
            return [Message];
        }
    }
}
