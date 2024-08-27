using AutoMapper;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Repos;
using CashFlow.Domain.Repos.Expenses;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionBase;

namespace CashFlow.App.Validations.Expenses.Register;
public class DeleteExpenseValidation : IDeleteExpenseValidation
{
    private readonly IExpenses _repos;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteExpenseValidation(IExpenses repos, IUnitOfWork unitOfWork)
    {
        _repos = repos;
        _unitOfWork = unitOfWork;
    }

    public async Task Execute(long id)
    {
        var result = await _repos.Delete(id);

        if (result == false)
        {
            throw new NotFoundException(ResourceErrorMessages.Expense_Not_Found);
        }

        await _unitOfWork.Commit();
    }
}
