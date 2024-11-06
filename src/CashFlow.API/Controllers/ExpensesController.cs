using CashFlow.App.Validations.Expenses.Delete;
using CashFlow.App.Validations.Expenses.GetAll;
using CashFlow.App.Validations.Expenses.GetById;
using CashFlow.App.Validations.Expenses.GetTitles;
using CashFlow.App.Validations.Expenses.GetTitlesByMonth;
using CashFlow.App.Validations.Expenses.GetTotalAmount;
using CashFlow.App.Validations.Expenses.Register;
using CashFlow.App.Validations.Expenses.Update;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CashFlow.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ExpensesController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseExpenseRegistered), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseError), StatusCodes.Status400BadRequest)]

    public async Task<IActionResult> Register(
        [FromServices] IRegisterExpenseValidation validation,
        [FromBody] RequestExpenses request)
    {

          var response = await validation.Execute(request);

          return Created(string.Empty, response);
      
    }

    [HttpGet]
    [ProducesResponseType(typeof(ResponseExpenses), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetAllExpenses([FromServices] IGetAllExpenseValidation validation)
    {
        var response = await validation.Execute();

        if (response.Expenses.Count != 0)
            return Ok(response);

        return NoContent();
    }

    [HttpGet("total-amount")]
    [ProducesResponseType(typeof(ResponseExpenses), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetTotalAmount([FromServices] IGetTotalAmount validation, [FromHeader] DateOnly month)
    {
        var response = await validation.Execute(month);
        return Ok(response);
    }

    [HttpGet("titles")]
    [ProducesResponseType(typeof(ResponseExpenses), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetTitles([FromServices] IGetTitles validation)
    {
        var response = await validation.Execute();
        return Ok(response);
    }

    [HttpGet("titles_by_month")]
    [ProducesResponseType(typeof(ResponseExpenses), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetTitlesByMonth([FromServices] IGetTitlesByMonth validation, [FromHeader] DateOnly month)
    {
        var response = await validation.Execute(month);
        return Ok(response);
    }

    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(typeof(ResponseExpense), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseError), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(
      [FromServices] IGetExpenseByIdValidation validation,
      [FromRoute] long id)
    {
        var response = await validation.Execute(id);

        return Ok(response);
    }

    [HttpDelete]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseError), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(
      [FromServices] IDeleteExpenseValidation validation,
      [FromRoute] long id)
    {
        await validation.Execute(id);

        return NoContent();
    }

    [HttpPut]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseError), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseError), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(
        [FromServices] IUpdateExpenseValidation validation,
        [FromRoute] long id,
        [FromBody] RequestExpenses request)
    {
        await validation.Execute(id, request);
        return NoContent();

    }
}
