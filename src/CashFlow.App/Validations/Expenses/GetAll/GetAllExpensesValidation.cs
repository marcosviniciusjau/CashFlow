using AutoMapper;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Entities;
using CashFlow.Domain.Repos.Expenses;
using CashFlow.Domain.Services;

namespace CashFlow.App.Validations.Expenses.GetAll;

public class GetAllExpensesValidation : IGetAllExpenseValidation
{
    private readonly IExpenseReadOnly _repo;
    private readonly IMapper _mapper;
    private readonly ILoggedUser _loggedUser;
    public GetAllExpensesValidation(
        IExpenseReadOnly repo,
        IMapper mapper,
        ILoggedUser loggedUser
        )
    {
        _repo = repo;
        _mapper = mapper;
        _loggedUser = loggedUser;
    }
    public async Task<ResponseExpenses> Execute()
    {
        var loggedUser = await _loggedUser.Get();
        var result = await _repo.GetAll(loggedUser);

        return new ResponseExpenses
        {
            Expenses = _mapper.Map<List<ResponseExpense>>(result)
        };
    }
}
