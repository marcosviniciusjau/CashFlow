using CashFlow.Domain.Entities;
using CashFlow.Domain.Repos.Expenses;
using CashFlow.Domain.Services;


namespace CashFlow.App.Validations.Expenses.GetTitlesByMonth;
public class GetTitlesByMonth : IGetTitlesByMonth
{
    private readonly IExpenseReadOnly _repos;

    private readonly ILoggedUser _loggedUser;
    public GetTitlesByMonth(IExpenseReadOnly repos, ILoggedUser loggedUser)
    {
        _repos = repos;
        _loggedUser = loggedUser;
    }

    public async Task<List<ExpenseDTO>> Execute(DateOnly month)
    {
        var loggedUser = await _loggedUser.Get();

        var titles = await _repos.GetTitlesWithAmountByMonth(loggedUser, month);
        return titles;
    }
}
