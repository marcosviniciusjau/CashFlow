namespace CashFlow.App.Validations.Expenses.Register;
public interface IDeleteExpenseValidation
{
    Task Execute(long id);
}
