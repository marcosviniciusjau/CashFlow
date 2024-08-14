using CashFlow.App.Validations.Expenses;
using CashFlow.App.Validations.Expenses.Register;
using CashFlow.Communication.Requests;
using Microsoft.AspNetCore.Mvc;

namespace CashFlow.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ExpensesController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Register(
        [FromServices] IRegisterExpenseValidation validation,
        [FromBody] RequestExpenses request)
    {

          var response = await validation.Execute(request);

          return Created(string.Empty, response);
      
    }
}
