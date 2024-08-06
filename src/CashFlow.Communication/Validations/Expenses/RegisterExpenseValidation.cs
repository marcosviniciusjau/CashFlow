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
        var emptyTitle = string.IsNullOrWhiteSpace(request.Title);
        if (emptyTitle)
        {
            throw new ArgumentException("Title is empty");
        }

        if (request.Amount <= 0)
        {
            throw new ArgumentException("Amount must be greater than 0");
        }

        var result = DateTime.Compare(request.Date, DateTime.UtcNow);

        if (result > 0)
        {
            throw new ArgumentException("Date cannot be in the future");
        }

        var paymentValid = Enum.IsDefined(typeof(PaymentTypes), request.PaymentType);

        if(!paymentValid)
        {
            throw new ArgumentException("Payment type not valid");
        }
    }

}
