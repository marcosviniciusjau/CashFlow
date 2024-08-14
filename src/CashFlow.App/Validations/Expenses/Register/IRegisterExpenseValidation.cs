using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;

namespace CashFlow.App.Validations.Expenses.Register;
public interface IRegisterExpenseValidation
{
    Task<ResponseExpenses> Execute(RequestExpenses request);
}
