using CashFlow.Communication.Enums;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;

namespace CashFlow.Communication.Validations.Expenses;

public class RegisterExpenseValidation
{
    public ResponseExpenses Execute(RequestExpenses request)
    {
        Validate(request);
        return new ResponseExpenses();
    }

    public void Validate(RequestExpenses request)
    {
       var validator = new RegisterExpenseValidator();
       var result = validator.Validate(request);
    }

}

