namespace CashFlow.Domain.Repos.Users;
public interface IUserReadOnly
{
    Task<bool> Exists(string email);
}
