using CashFlow.Communication.Responses;

namespace CashFlow.App.Validations.Users.GetProfile;
public interface IGetProfileValidation
{
    public Task<ResponseUserProfile> Execute();
}
