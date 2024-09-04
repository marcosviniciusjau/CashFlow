using CashFlow.Domain.Entities;

namespace CashFlow.Domain.Security
{
    public interface ITokenGenerator
    {
        string Generate(User user);
    }
}
