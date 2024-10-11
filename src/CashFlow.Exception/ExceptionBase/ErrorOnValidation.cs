using System.Net;

namespace CashFlow.Exception.ExceptionBase;
public class ErrorOnValidation : CashFlowException
{
    private readonly List<string> _errors;
    public override int StatusCode => (int)HttpStatusCode.BadRequest;
    public ErrorOnValidation(List<string> errorMessages) : base(string.Empty)
    {
        _errors = errorMessages;
    }

    public override List<string> GetErrors()
    {
        return _errors;
    }
}