using CashFlow.Domain.Entities;
using CashFlow.Domain.Repos.Expenses;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Infra.DataAccess.Repos;

internal class ExpensesRepo : IExpenseReadOnly,IExpensesWrite, IExpensesUpdate
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

    public async Task<bool> Delete(long id)
    {
        var result = await _dbContext.Expenses.FirstOrDefaultAsync(expense => expense.Id == id);
        if (result is null)
        {
            return false;
        }

        _dbContext.Expenses.Remove(result);

        return true;
    }


    public void Update(Expense expense)
    {
        _dbContext.Expenses.Update(expense);
    }

     async Task<Expense?> IExpenseReadOnly.GetById(long id)
    {
        return await _dbContext.Expenses
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.Id == id);
    } 
     async Task<Expense?> IExpensesUpdate.GetById(long id)
    {
        return await _dbContext.Expenses
            .FirstOrDefaultAsync(e => e.Id == id);
    }

}
