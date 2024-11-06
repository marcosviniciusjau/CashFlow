using CashFlow.Domain.Entities;

namespace CashFlow.Domain.Repos.Expenses;
public interface IExpenseReadOnly
{
    Task<List<Expense>> GetAll(User user);
    Task<List<ExpenseDTO>> GetTitlesWithAmount(User user);
    Task<List<ExpenseDTO>> GetTitlesWithAmountByMonth(User user, DateOnly date);
    Task<Expense?> GetById(User user,long id);
    Task<List<Expense>> FilterByMonth(User user,DateOnly date);
}
