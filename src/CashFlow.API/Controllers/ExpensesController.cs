using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
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
        try
        {
            var validation = new RegisterExpenseValidation();

            var response = validation.Execute(request);

            return Created(string.Empty, response);
        }
        catch(ArgumentException ex)
        {
            var errorResponse = new ResponseError(ex.Message);
            return BadRequest(errorResponse);
        }
        catch
        {
            var errorResponse = new ResponseError("unknow error");
            
            return StatusCode(StatusCodes.Status500InternalServerError, errorResponse);
        }
    }
}
