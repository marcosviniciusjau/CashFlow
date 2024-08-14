using CashFlow.Domain.Entities;

namespace CashFlow.Domain.Repos.Expenses;
public interface IExpenses
{
    Task Add(Expense expense);
    Task Delete(Expense expense);
    Task Update(Expense expense);
    Task <List<Expense>> GetAll();
    Task<Expense?> GetById(long id);
}
