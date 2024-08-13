using CashFlow.Domain.Entities;
using CashFlow.Domain.Repos.Expenses;

namespace CashFlow.Infra.DataAccess.Repos;

internal class ExpensesRepo : IExpenses
{
    private readonly CashFlowDbContext _dbContext;
    public ExpensesRepo(CashFlowDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public void Add(Expense expense)
    {
        _dbContext.Expenses.Add(expense);
    }

    public void Delete(Expense expense)
    {
        throw new NotImplementedException();
    }

    public void Update(Expense expense)
    {
        throw new NotImplementedException();
    }

    public List<Expense> Get()
    {
        throw new NotImplementedException();
    }

    public Expense GetById(int id)
    {
        throw new NotImplementedException();
    }

}
