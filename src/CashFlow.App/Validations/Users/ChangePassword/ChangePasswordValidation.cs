using AutoMapper;
using CashFlow.App.Validations.Users.Register;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Entities;
using CashFlow.Domain.Repos.Users;
using CashFlow.Domain.Repos;
using CashFlow.Domain.Security;
using CashFlow.Exception.ExceptionBase;
using CashFlow.Exception;
using FluentValidation.Results;
using CashFlow.Domain.Services;
using CashFlow.Communication.Requests;

namespace CashFlow.App.Validations.Users.ChangePassword;
public class ChangePasswordValidation(ILoggedUser loggedUser,
    IPasswordEncripter passwordEncripter,
    IUserUpdate repos,
    IUnitOfWork unitOfWork
        ) : IChangePasswordValidation
{
    private readonly ILoggedUser _loggedUser = loggedUser;
    private readonly IPasswordEncripter _passwordEncripter = passwordEncripter;
    private readonly IUserUpdate _repos = repos;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
  
    public async Task Execute(RequestChangePassword request)
    {
        var loggedUser = await _loggedUser.Get();
        Validate(request, loggedUser);

        var user = await _repos.GetById(loggedUser.Id);
        user.Password = _passwordEncripter.Encrypt(request.NewPassword);

        _repos.Update(user);
        await _unitOfWork.Commit();
    }

    private void Validate(RequestChangePassword request, User loggedUser)
    {
        var validator = new ChangePasswordValidator();

        var result = validator.Validate(request);
        var passwordMatch = _passwordEncripter.Verify(request.Password, loggedUser.Password);
      
        if (passwordMatch == false)
        {
            result.Errors.Add(new ValidationFailure(string.Empty, ResourceErrorMessages.Different_Password));
        }

        if (result.IsValid == false)
        {
            var errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList();

            throw new ErrorOnValidation(errorMessages);
        }
    }
}