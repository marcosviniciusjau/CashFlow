using CashFlow.Domain.Entities;
using CashFlow.Domain.Repos.Expenses;

namespace CashFlow.Infra.DataAccess.Repos;

internal class ExpensesRepo : IExpenses
{
    public void Add(Expense expense)
    {
        var dbContext = new CashFlowDbContext();
        dbContext.Expenses.Add(expense);
        dbContext.SaveChanges();
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
