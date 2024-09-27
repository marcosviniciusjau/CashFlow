using AutoMapper;
using CashFlow.Domain.Repos;
using CashFlow.Domain.Repos.Users;
using CashFlow.Domain.Services;

namespace CashFlow.App.Validations.Users.Delete;
public class DeleteProfileValidation : IDeleteProfileValidation
{

    private readonly ILoggedUser _loggedUser;
    private readonly IUserWrite _repos;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteProfileValidation(
        ILoggedUser loggedUser,
        IUserWrite repos,
        IUnitOfWork unitOfWork)
    {
        _loggedUser = loggedUser;
        _repos = repos;
        _unitOfWork = unitOfWork;
    }

    public async Task Execute()
    {
        var user = await _loggedUser.Get();

        await _repos.Delete(user);

        await _unitOfWork.Commit();
    }
}