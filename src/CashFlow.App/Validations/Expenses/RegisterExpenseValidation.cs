using CashFlow.Communication.Requests;
using CashFlow.Exception.ExceptionsBase;

namespace CashFlow.App.Validations.Expenses;
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