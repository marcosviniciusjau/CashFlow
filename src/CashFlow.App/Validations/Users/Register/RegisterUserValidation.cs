using AutoMapper;
using CashFlow.App.Validations.Users;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Entities;
using CashFlow.Domain.Repos;
using CashFlow.Domain.Repos.Expenses;
using CashFlow.Domain.Repos.Users;
using CashFlow.Domain.Security;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionBase;
using FluentValidation.Results;

namespace CashFlow.App.Validations.Users.Register;
public class RegisterUserValidation : IRegisterUserValidation
{
    private readonly IMapper _mapper;
    private readonly IPasswordEncripter _passwordEncripter;
    private readonly IUserReadOnly _userReadOnly;
    public RegisterUserValidation(IMapper mapper,IPasswordEncripter passwordEncripter, IUserReadOnly userReadOnly)
    {
        _mapper = mapper;
        _passwordEncripter = passwordEncripter;
        _userReadOnly = userReadOnly;
    }

    public async Task<ResponseRegisteredUser> Execute(RequestUser request)
    {
        await Validate(request);

        var user = _mapper.Map<User>(request);
        user.Password = _passwordEncripter.Encrypt(request.Password);
        return new ResponseRegisteredUser
        {
            Name = user.Name
        };
    }

    private async Task Validate(RequestUser request)
    {
        var validator = new UserValidator();

        var result = validator.Validate(request);
        var exists = await _userReadOnly.Exists(request.Email);
        if (exists) {
            result.Errors.Add(new ValidationFailure(string.Empty,ResourceErrorMessages.Email_Exists));
        }

        if (result.IsValid == false)
        {
            var errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList();

            throw new ErrorOnValidation(errorMessages);
        }
    }
}