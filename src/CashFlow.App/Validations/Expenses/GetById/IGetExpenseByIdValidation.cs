using CashFlow.Communication.Responses;

namespace CashFlow.App.Validations.Expenses.GetById;
public interface IGetExpenseByIdValidation
{
    Task<ResponseExpense> Execute(long id);
}