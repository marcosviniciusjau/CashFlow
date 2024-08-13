using CashFlow.App.Validations.Expenses.Register;
using Microsoft.Extensions.DependencyInjection;

namespace CashFlow.App;

public static class DependencyInjection
{
    public static void AddApp(this IServiceCollection services)
    {
        services.AddScoped<IRegisterExpenseValidation, RegisterExpenseValidation>();
    }
}
