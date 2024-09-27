using CashFlow.Domain.Entities;

namespace CashFlow.Domain.Repos.Expenses;
public interface IExpensesWrite
{
    Task Add(Expense expense);
    Task Delete(long id);
}
