﻿using AutoMapper;
using CashFlow.Communication.Requests;
using CashFlow.Domain.Repos;
using CashFlow.Domain.Repos.Expenses;
using CashFlow.Domain.Services;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionBase;

namespace CashFlow.App.Validations.Expenses.Update;
public class UpdateExpenseValidation(
    IExpensesUpdate repo,
    IUnitOfWork unityOfWork,
    IMapper mapper,
    ILoggedUser loggedUser
    ) : IUpdateExpenseValidation
{
    private readonly IUnitOfWork _unityOfWork = unityOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly IExpensesUpdate _repo = repo;
    private readonly ILoggedUser _loggedUser = loggedUser;

    public async Task Execute(long id,RequestExpenses request)
    {
        Validate(request);
        var loggedUser = await _loggedUser.Get();

        var expense = await _repo.GetById(loggedUser,id);
        if (expense is null)
        {
            throw new NotFoundException(ResourceErrorMessages.Expense_Not_Found);
        }

        expense.Tags.Clear();

        _mapper.Map(request, expense);

         _repo.Update(expense);

        await _unityOfWork.Commit();
    }

    private static void Validate(RequestExpenses request)
    {
        var validator = new ExpenseValidator();

        var result = validator.Validate(request);

        if (result.IsValid == false)
        {
            var errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList();

            throw new ErrorOnValidation(errorMessages);
        }
    }
}