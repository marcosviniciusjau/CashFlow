using AutoMapper;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Repos.Users;
using CashFlow.Domain.Repos;
using CashFlow.Exception.ExceptionBase;
using CashFlow.Exception;
using FluentValidation.Results;
using CashFlow.Domain.Services;

namespace CashFlow.App.Validations.Users.Update;
public class UpdateProfileValidation(
    IUserReadOnly userReadOnly,
    IUserUpdate repos,
    ILoggedUser loggedUser,
    IUnitOfWork unitOfWork
 ) : IUpdateProfileValidation
{
    private readonly IUserReadOnly _userReadOnly = userReadOnly;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ILoggedUser _loggedUser = loggedUser;
    private readonly IUserUpdate _repos = repos;
    public async Task Execute(RequestUpdateUser request)
    {
        var loggedUser = await _loggedUser.Get();
        await Validate(request, loggedUser.Email);

        var user = await _repos.GetById(loggedUser.Id);
        user.Name = request.Name;
        user.Email = request.Email;

        _repos.Update(user);

        await _unitOfWork.Commit();
    }

    private async Task Validate(RequestUpdateUser request, string currentEmail)
    {
        var validator = new UpdateUserValidator();

        var result = validator.Validate(request);
        if (currentEmail.Equals(request.Email) == false)
        {
            var userExist = await _repos.ExistUser(request.Email);
            if (userExist)
            {
                result.Errors.Add(new ValidationFailure(string.Empty, ResourceErrorMessages.Email_Exists));
            }
        }

        if (result.IsValid == false)
        {
            var errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList();

            throw new ErrorOnValidation(errorMessages);
        }
    }
}