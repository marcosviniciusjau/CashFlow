using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using CashFlow.Communication.Validations.Expenses;
using CashFlow.Exception.ExceptionBase;

namespace CashFlow.Application.UseCases.Expenses.Register;
public class RegisterExpenseValidation
{
    public RegisterExpenseValidation Execute(RequestExpenses request)
    {
        Validate(request);

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