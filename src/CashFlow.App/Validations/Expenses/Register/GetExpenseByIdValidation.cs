﻿using AutoMapper;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Repos.Expenses;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionBase;

namespace CashFlow.App.Validations.Expenses.Register;
public class GetExpenseByIdValidation : IGetExpenseByIdValidation
{
    private readonly IExpenses _repos;
    private readonly IMapper _mapper;

    public GetExpenseByIdValidation(IExpenses repos, IMapper mapper)
    {
        _repos = repos;
        _mapper = mapper;
    }

    public async Task<ResponseExpense> Execute(long id)
    {
        var result = await _repos.GetById(id);

        return result is null ? throw new NotFoundException(ResourceErrorMessages.Expense_Not_Found) : _mapper.Map<ResponseExpense>(result);
    }
}
