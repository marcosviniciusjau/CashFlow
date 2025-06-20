using CashFlow.Domain.Repos;
using CashFlow.Domain.Repos.Expenses;
using CashFlow.Domain.Repos.Users;
using CashFlow.Domain.Security;
using CashFlow.Domain.Services;
using CashFlow.Infra.DataAccess;
using CashFlow.Infra.DataAccess.Repos;
using CashFlow.Infra.Security;
using CashFlow.Infra.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.SqlServer.Management.Common;

namespace CashFlow.Infra;

public static class DependencyInjectionExtension
{
    public static void AddInfra(this IServiceCollection services, IConfiguration config)
    {
        services.AddScoped<ILoggedUser, LoggedUser>();
        services.AddScoped<IPasswordEncripter, Security.BCrypt>();

        AddRepos(services, config);
        AddDbContext(services, config);
        AddToken(services, config);

    }

    private static void AddToken(IServiceCollection services, IConfiguration configuration)
    {
        var expirationMinutes = configuration.GetValue<uint>("Settings:Jwt:ExpiresMinutes");
        var signingKey = configuration.GetValue<string>("Settings:Jwt:SigningKey");

        services.AddScoped<ITokenGenerator>(config => new TokenGenerator(expirationMinutes, signingKey!));
    }

    private static void AddRepos(IServiceCollection services, IConfiguration config)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IExpensesWrite, ExpensesRepo>();
        services.AddScoped<IExpenseReadOnly, ExpensesRepo>();
        services.AddScoped<IExpensesUpdate, ExpensesRepo>();
        services.AddScoped<IUserReadOnly, UsersRepo>();
        services.AddScoped<IUserWrite, UsersRepo>();
        services.AddScoped<IUserUpdate, UsersRepo>();
    }

    private static void AddDbContext(IServiceCollection services, IConfiguration config)
    {
        var connectionString = config.GetConnectionString("Connection");
        services.AddDbContext<CashFlowDbContext>(config => config.UseSqlServer(connectionString), ServiceLifetime.Scoped);
    }
}
