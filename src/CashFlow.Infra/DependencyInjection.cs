﻿using CashFlow.Domain.Repos;
using CashFlow.Domain.Repos.Expenses;
using CashFlow.Infra.DataAccess;
using CashFlow.Infra.DataAccess.Repos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CashFlow.Infra;

public static class DependencyInjection
{
    public static void AddInfra(this IServiceCollection services, IConfiguration config)
    {
        AddRepos(services, config);
        AddDbContext(services, config);
    }

    private static void AddRepos(IServiceCollection services, IConfiguration config)
    {
        services.AddScoped<IUnityOfWork, UnitOfWork>();
        services.AddScoped<IExpenses, ExpensesRepo>();
    }

    private static void AddDbContext(IServiceCollection services, IConfiguration config)
    {
        var connectionString = config.GetConnectionString("Connection");
        var serverVersion = new MySqlServerVersion(new Version(8, 0, 38));
     
        services.AddDbContext<CashFlowDbContext>(config => config.UseMySql(connectionString, serverVersion));
    }
}
