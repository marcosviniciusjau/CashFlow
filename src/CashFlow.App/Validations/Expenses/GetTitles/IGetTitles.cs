using CashFlow.Domain.Entities;

namespace CashFlow.App.Validations.Expenses.GetTitles;
public interface IGetTitles
{
    Task<List<ExpenseDTO>> Execute();
}
