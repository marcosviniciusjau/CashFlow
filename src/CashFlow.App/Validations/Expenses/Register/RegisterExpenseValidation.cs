using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Entities;
using CashFlow.Domain.Repos;
using CashFlow.Domain.Repos.Expenses;
using CashFlow.Exception.ExceptionsBase;

namespace CashFlow.App.Validations.Expenses.Register;
public class RegisterExpenseValidation : IRegisterExpenseValidation
{
    private readonly IExpenses _repo;
    private readonly IUnityOfWork _unityOfWork;
    public RegisterExpenseValidation(IExpenses repo, IUnityOfWork unityOfWork)
    {
        _repo = repo;
        _unityOfWork = unityOfWork;
    }

    public ResponseExpenses Execute(RequestExpenses request)
    {
        Validate(request);

        var entity = new Expense
        {
            Title = request.Title,
            Description = request.Description,
            Amount = request.Amount,
            Date = request.Date,
            PaymentType = (Domain.Entities.Enums.PaymentTypes)request.PaymentType
        };

        _repo.Add(entity);

        _unityOfWork.Commit();
        return new ResponseExpenses();
    }

    private void Validate(RequestExpenses request)
    {
        var validator = new RegisterExpenseValidator();

        var result = validator.Validate(request);

        if (result.IsValid == false)
        {
            var errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList();

            throw new ErrorOnValidation(errorMessages);
        }
    }
}