using CashFlow.Domain.Entities;

namespace CashFlow.Domain.Repos.Expenses;
public interface IExpensesUpdate
{
    Task<Expense?> GetById(long id);
    void Update(Expense expense);
}
