using AutoMapper;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Repos.Expenses;
using CashFlow.Domain.Services;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionBase;

namespace CashFlow.App.Validations.Expenses.GetById;
public class GetExpenseByIdValidation : IGetExpenseByIdValidation
{
    private readonly IExpenseReadOnly _repos;
    private readonly IMapper _mapper;
    private readonly ILoggedUser _loggedUser;

    public GetExpenseByIdValidation(
        IExpenseReadOnly repos,
        IMapper mapper,
        ILoggedUser loggedUser
        )
    {
        _repos = repos;
        _mapper = mapper;
        _loggedUser = loggedUser;
    }

    public async Task<ResponseExpense> Execute(long id)
    {
        var loggedUser = await _loggedUser.Get();
        var result = await _repos.GetById(loggedUser, id);

        return result is null ? throw new NotFoundException(ResourceErrorMessages.Expense_Not_Found) : _mapper.Map<ResponseExpense>(result);
    }
}
