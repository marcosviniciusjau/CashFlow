using CashFlow.Domain.Entities;

namespace CashFlow.Domain.Repos.Users;
public interface IUserWrite
{
    Task Add(User user);
}
