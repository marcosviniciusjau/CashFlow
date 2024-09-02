using CashFlow.Domain.Repos.Users;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Infra.DataAccess.Repos;

internal class UsersRepo : IUserReadOnly
{
    private readonly CashFlowDbContext _dbContext;
    public UsersRepo(CashFlowDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> Exists(string email)
    {
        return await _dbContext.Users.AnyAsync(u => u.Email.Equals(email));
    }
}
