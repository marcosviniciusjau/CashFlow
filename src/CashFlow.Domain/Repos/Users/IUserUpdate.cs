using CashFlow.Domain.Entities;

namespace CashFlow.Domain.Repos.Users;
public interface IUserUpdate
{
    Task<bool> ExistUser(string email);
    Task<User> GetById(long id);
    void Update(User user);
}
