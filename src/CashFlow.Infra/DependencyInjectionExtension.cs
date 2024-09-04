using CashFlow.Domain.Repos;
using CashFlow.Domain.Repos.Expenses;
using CashFlow.Domain.Repos.Users;
using CashFlow.Domain.Security;
using CashFlow.Infra.DataAccess;
using CashFlow.Infra.DataAccess.Repos;
using CashFlow.Infra.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CashFlow.Infra;

public static class DependencyInjectionExtension
{
    public static void AddInfra(this IServiceCollection services, IConfiguration config)
    {
        AddRepos(services, config);
        AddDbContext(services, config);
        AddToken(services, config);

        services.AddScoped<IPasswordEncripter, Security.BCrypt>();
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
    }

    private static void AddDbContext(IServiceCollection services, IConfiguration config)
    {
        var connectionString = config.GetConnectionString("Connection");
        var serverVersion = new MySqlServerVersion(new Version(8, 0, 38));
     
        services.AddDbContext<CashFlowDbContext>(config => config.UseMySql(connectionString, serverVersion));
    }
}
