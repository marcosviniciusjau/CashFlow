using CashFlow.Communication.Requests;

namespace CashFlow.App.Validations.Expenses.Update;
public interface IUpdateExpenseValidation
{
    Task Execute(long id, RequestExpenses request);
}
