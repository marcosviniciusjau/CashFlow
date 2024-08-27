using CashFlow.Communication.Responses;

namespace CashFlow.App.Validations.Expenses.GetAll;
public interface IGetAllExpenseValidation
{
    Task<ResponseExpenses> Execute();
}
