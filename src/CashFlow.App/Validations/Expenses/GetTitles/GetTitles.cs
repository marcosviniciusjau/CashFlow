using CashFlow.Domain.Entities;
using CashFlow.Domain.Repos.Expenses;
using CashFlow.Domain.Services;


namespace CashFlow.App.Validations.Expenses.GetTitles;
public class GetTitles : IGetTitles
{
    private readonly IExpenseReadOnly _repos;

    private readonly ILoggedUser _loggedUser;
    public GetTitles(IExpenseReadOnly repos, ILoggedUser loggedUser)
    {
        _repos = repos;
        _loggedUser = loggedUser;
    }

    public async Task<List<ExpenseDTO>> Execute()
    {
        var loggedUser = await _loggedUser.Get();

        var titles = await _repos.GetTitlesWithAmount(loggedUser);
        return titles;
    }
}
