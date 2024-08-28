using CashFlow.App.AutoMapper;
using CashFlow.App.Validations.Expenses.Delete;
using CashFlow.App.Validations.Expenses.GetAll;
using CashFlow.App.Validations.Expenses.GetById;
using CashFlow.App.Validations.Expenses.Register;
using CashFlow.App.Validations.Expenses.Update;
using CashFlow.App.Validations.Reports;
using Microsoft.Extensions.DependencyInjection;

namespace CashFlow.App;

public static class DependencyInjection
{
    public static void AddApp(this IServiceCollection services)
    {
       AddAutoMapper(services);

       AddValidations(services);
    }

    private static void AddAutoMapper(IServiceCollection services)
    {
        services.AddAutoMapper(typeof(AutoMapping));
    }

    private static void AddValidations(IServiceCollection services)
    {
        services.AddScoped<IRegisterExpenseValidation, RegisterExpenseValidation>();
        services.AddScoped<IGetAllExpenseValidation, GetAllExpensesValidation>();
        services.AddScoped<IGetExpenseByIdValidation, GetExpenseByIdValidation>();
        services.AddScoped<IDeleteExpenseValidation, DeleteExpenseValidation>();
        services.AddScoped<IUpdateExpenseValidation, UpdateExpenseValidation>();
        services.AddScoped<IGenerateReportExcelValidation, GenerateReportExcelValidation>();
    }
}
