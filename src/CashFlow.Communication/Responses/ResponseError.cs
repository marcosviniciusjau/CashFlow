namespace CashFlow.Communication.Responses;
public class ResponseError
{
    public string ErrorMessage { get; set; } = string.Empty;

    public ResponseError(string errorMessage)
    {
        ErrorMessage = errorMessage;
    }
}