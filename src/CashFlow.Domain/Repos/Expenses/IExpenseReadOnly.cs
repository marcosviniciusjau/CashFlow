using CashFlow.Domain.Entities;

namespace CashFlow.Domain.Repos.Expenses;
public interface IExpenseReadOnly
{
    Task<List<Expense>> GetAll(User user);
    Task<Expense?> GetById(User user,long id);
    Task<List<Expense>> FilterByMonth(User user,DateOnly date);
}
