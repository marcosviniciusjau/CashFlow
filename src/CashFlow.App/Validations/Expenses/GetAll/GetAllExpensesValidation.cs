using AutoMapper;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Entities;
using CashFlow.Domain.Repos.Expenses;

namespace CashFlow.App.Validations.Expenses.GetAll;

public class GetAllExpensesValidation : IGetAllExpenseValidation
{
    private readonly IExpenseReadOnly _repo;
    private readonly IMapper _mapper;
    public GetAllExpensesValidation(IExpenseReadOnly repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }
    public async Task<ResponseExpenses> Execute()
    {
        var result = await _repo.GetAll();

        return new ResponseExpenses
        {
            Expenses = _mapper.Map<List<ResponseShortExpense>>(result)
        };
    }
}
