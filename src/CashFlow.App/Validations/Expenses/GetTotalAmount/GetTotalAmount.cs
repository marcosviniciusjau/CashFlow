using CashFlow.Domain.Repos.Expenses;
using CashFlow.Domain.Services;


namespace CashFlow.App.Validations.Expenses.GetTotalAmount;
public class GetTotalAmount: IGetTotalAmount
{
    private readonly IExpenseReadOnly _repos;

    private readonly ILoggedUser _loggedUser;
    public GetTotalAmount(IExpenseReadOnly repos, ILoggedUser loggedUser)
    {
        _repos = repos;
        _loggedUser = loggedUser;
    }

    public async Task<decimal> Execute(DateOnly month)
    {
        var loggedUser = await _loggedUser.Get();

        var expenses = await _repos.FilterByMonth(loggedUser, month);
        if (expenses.Count == 0)
        {
            return 0;
        }


        var totalExpenses = expenses.Sum(expense => expense.Amount);
        return totalExpenses;
    }
}
