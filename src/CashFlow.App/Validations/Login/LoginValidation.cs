using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Repos.Users;
using CashFlow.Domain.Security;
using CashFlow.Exception.ExceptionBase;

namespace CashFlow.App.Validations.Login;
public class LoginValidation : ILoginValidation
{
    private readonly IUserReadOnly _repos;
    private readonly IPasswordEncripter _passwordEncripter;
    private readonly ITokenGenerator _tokenGenerator;

    public LoginValidation(IUserReadOnly repos, IPasswordEncripter passwordEncripter, ITokenGenerator tokenGenerator)
    {
        _repos = repos;
        _passwordEncripter = passwordEncripter;
        _tokenGenerator = tokenGenerator;
    }

    public async Task<ResponseRegisteredUser> Execute(RequestLogin request)
    {
        var user = await _repos.GetByEmail(request.Email);
        if (user == null)
        {
            throw new InvalidLogin();
        }
        var passwordMatch = _passwordEncripter.Verify(request.Password, user.Password);
        if(passwordMatch == false)
        {
            throw new InvalidLogin();
        }

        return new ResponseRegisteredUser
        {
            Name = user.Name,
            Token = _tokenGenerator.Generate(user)
        };
    }
}
