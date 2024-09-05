namespace CashFlow.Domain.Security;
public interface ITokenProvider
{
    string TokenOnRequest();
}