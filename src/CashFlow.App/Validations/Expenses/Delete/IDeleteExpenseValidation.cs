namespace CashFlow.App.Validations.Expenses.Delete;
public interface IDeleteExpenseValidation
{
    Task Execute(long id);
}
