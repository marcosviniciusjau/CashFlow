using AutoMapper;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Entities;
using CashFlow.Domain.Repos;
using CashFlow.Domain.Repos.Expenses;
using CashFlow.Domain.Services;
using CashFlow.Exception.ExceptionBase;
using PdfSharp.Drawing;

namespace CashFlow.App.Validations.Expenses.Register;
public class RegisterExpenseValidation : IRegisterExpenseValidation
{
    private readonly IExpensesWrite _repo;
    private readonly IUnitOfWork _unityOfWork;
    private readonly IMapper _mapper;
    private readonly ILoggedUser _loggedUser;
    public RegisterExpenseValidation(
        IExpensesWrite repo,
        IUnitOfWork unityOfWork,
        IMapper mapper,
        ILoggedUser loggedUser
        )
    {
        _repo = repo;
        _unityOfWork = unityOfWork;
        _mapper = mapper;
        _loggedUser = loggedUser;
    }

    public async Task<ResponseExpense> Execute(RequestExpenses request)
    {
        Validate(request);
        var loggedUser = await _loggedUser.Get();

        var entity = _mapper.Map<Expense>( request ); 
        entity.UserId = loggedUser.Id;

        await _repo.Add(entity);

        await _unityOfWork.Commit();
        return _mapper.Map<ResponseExpense>(entity);
    }

    private void Validate(RequestExpenses request)
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