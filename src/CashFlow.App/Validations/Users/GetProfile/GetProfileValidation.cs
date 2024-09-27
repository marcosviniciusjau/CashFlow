using AutoMapper;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Services;

namespace CashFlow.App.Validations.Users.GetProfile;
public class GetProfileValidation : IGetProfileValidation
{
    private readonly ILoggedUser _loggedUser;
    private readonly IMapper _mapper;
    public GetProfileValidation(ILoggedUser loggedUser, IMapper mapper)
    {
        _loggedUser = loggedUser;
        _mapper = mapper;
    }

    public async Task<ResponseUserProfile> Execute()
    {
        var user = await _loggedUser.Get();
        return _mapper.Map<ResponseUserProfile>(user);
    }
}
