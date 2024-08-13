using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;

namespace CashFlow.App.Validations.Expenses.Register;
public interface IRegisterExpenseValidation
{
    ResponseExpenses Execute(RequestExpenses request);
}
