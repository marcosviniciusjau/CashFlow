using CashFlow.Communication.Requests;
using CashFlow.Domain.Entities;
using CashFlow.Exception.ExceptionsBase;
using CashFlow.Infra.DataAccess;

namespace CashFlow.App.Validations.Expenses;
public class RegisterExpenseValidation
{
    public RegisterExpenseValidation Execute(RequestExpenses request)
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
        
        dbContext.Expenses.Add(entity);
        dbContext.SaveChanges();
        return new RegisterExpenseValidation();
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