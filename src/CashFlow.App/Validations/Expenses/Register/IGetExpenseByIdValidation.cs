using CashFlow.Communication.Responses;

namespace CashFlow.App.Validations.Expenses.Register;
public interface IGetExpenseByIdValidation
{
    Task<ResponseExpense> Execute(long id);
}