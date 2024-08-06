using CashFlow.Communication.Requests;
using CashFlow.Communication.Validations.Expenses;
using Microsoft.AspNetCore.Mvc;

namespace CashFlow.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ExpensesController : ControllerBase
{
    [HttpPost]
    public IActionResult Register([FromBody]RequestExpenses request)
    {
        var validation = new RegisterExpenseValidation();

        var response = validation.Execute(request);

        return Created(string.Empty, response);
    }
}
