using CashFlow.Exception.ExceptionBase;

namespace CashFlow.Exception.ExceptionsBase;
public class ErrorOnValidation : CashFlowException
{
    public List<string> Errors { get; set; }

    public ErrorOnValidation(List<string> errorMessages)
    {
        Errors = errorMessages;
    }
}