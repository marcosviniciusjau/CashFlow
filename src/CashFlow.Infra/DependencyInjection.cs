using CashFlow.Domain.Repos.Expenses;
using CashFlow.Infra.DataAccess.Repos;
using Microsoft.Extensions.DependencyInjection;

namespace CashFlow.Infra;

public static class DependencyInjection
{
    public static void AddInfra(this IServiceCollection services)
    {
        services.AddScoped<IExpenses, ExpensesRepo>();
    }
}
