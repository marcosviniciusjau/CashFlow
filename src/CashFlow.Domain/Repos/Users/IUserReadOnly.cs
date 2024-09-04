using CashFlow.Domain.Entities;

namespace CashFlow.Domain.Repos.Users;
public interface IUserReadOnly
{
    Task<bool> Exists(string email);
    Task<User?> GetByEmail(string email);
}
