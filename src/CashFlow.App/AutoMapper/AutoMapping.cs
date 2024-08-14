using AutoMapper;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Entities;

namespace CashFlow.App.AutoMapper;

public class AutoMapping: Profile
{
    public AutoMapping()
    {
        RequestToEntity();
        EntityToResponse();
    }

    private void RequestToEntity()
    {
        CreateMap<RequestExpenses, Expense>();
    }

    private void EntityToResponse()
    {
        CreateMap<Expense, ResponseExpense>();
        CreateMap<Expense, ResponseShortExpense>();
    }
}
