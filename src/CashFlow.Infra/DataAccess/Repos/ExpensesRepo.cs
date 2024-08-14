using CashFlow.Domain.Entities;
using CashFlow.Domain.Repos.Expenses;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Infra.DataAccess.Repos;

internal class ExpensesRepo : IExpenses
{
    private readonly CashFlowDbContext _dbContext;
    public ExpensesRepo(CashFlowDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task Add(Expense expense)
    {
        await _dbContext.Expenses.AddAsync(expense);
    }
    public async Task<List<Expense>> GetAll()
    {
        return await _dbContext.Expenses.AsNoTracking().ToListAsync();

    }

    public async Task Delete(Expense expense)
    {
        throw new NotImplementedException();
    }

    public async Task Update(Expense expense)
    {
        throw new NotImplementedException();
    }

    public async Task<Expense?> GetById(long id)
    {
        return await _dbContext.Expenses
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.Id == id);
    }

}
