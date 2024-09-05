using CashFlow.Domain.Entities;

namespace CashFlow.Domain.Repos.Expenses;
public interface IExpensesUpdate
{
    Task<Expense?> GetById(User user, long id);
    void Update(Expense expense);
}
