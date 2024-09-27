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
        CreateMap<RequestExpenses, Expense>()
            .ForMember(dest => dest.Tags, config => config.MapFrom(source => source.Tags.Distinct()));
        ;
        CreateMap<RequestUser, User>()
            .ForMember(dest => dest.Password, opt => opt.Ignore());
        CreateMap<Communication.Enums.Tag, Tag>()
            .ForMember(dest => dest.Value, config => config.MapFrom(source => source));
    }

    private void EntityToResponse()
    {
        CreateMap<Expense, ResponseExpense>()
            .ForMember(dest => dest.Tags, config => config.MapFrom(source => source.Tags.Select(tag => tag.Value)));
       
        CreateMap<Expense, ResponseShortExpense>();
        CreateMap<Expense, ResponseExpenseRegistered>();
        CreateMap<Expense, ResponseRegisteredUser>();
        CreateMap<User, ResponseUserProfile>();
    }
}
