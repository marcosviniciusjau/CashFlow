using AutoMapper;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Repos;
using CashFlow.Domain.Repos.Expenses;
using CashFlow.Domain.Services;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionBase;

namespace CashFlow.App.Validations.Expenses.Delete;
public class DeleteExpenseValidation : IDeleteExpenseValidation
{
    private readonly IExpensesWrite _repos;

    private readonly IExpenseReadOnly _expenseReadOnly;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILoggedUser _loggedUser;

    public DeleteExpenseValidation(
        IExpensesWrite repos,
        IUnitOfWork unitOfWork,
        ILoggedUser loggedUser,
        IExpenseReadOnly expenseReadOnly
        )
    {
        _repos = repos;
        _unitOfWork = unitOfWork;
        _loggedUser = loggedUser;
        _expenseReadOnly = expenseReadOnly;
    }

    public async Task Execute(long id)
    {
        var loggedUser = await _loggedUser.Get();
        var expense = _expenseReadOnly.GetById(loggedUser, id);

        if (expense is null)
        {
            throw new NotFoundException(ResourceErrorMessages.Expense_Not_Found);
        }
        await _repos.Delete(id);
        await _unitOfWork.Commit();
    }
}
