namespace CashFlow.App.Validations.Expenses.GetTotalAmount;
public interface IGetTotalAmount
{
    Task<decimal> Execute(DateOnly month);
}
