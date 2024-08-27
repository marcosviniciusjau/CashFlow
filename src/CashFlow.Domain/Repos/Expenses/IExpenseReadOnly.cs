using CashFlow.Domain.Entities;

namespace CashFlow.Domain.Repos.Expenses;
public interface IExpenseReadOnly
{
    Task<List<Expense>> GetAll();
    Task<Expense?> GetById(long id);
}
