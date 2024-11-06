using CashFlow.Domain.Entities;

namespace CashFlow.App.Validations.Expenses.GetTitlesByMonth;
public interface IGetTitlesByMonth
{
    Task<List<ExpenseDTO>> Execute(DateOnly month);
}
