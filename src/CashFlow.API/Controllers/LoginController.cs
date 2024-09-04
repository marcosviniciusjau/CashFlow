using CashFlow.App.Validations.Login;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using Microsoft.AspNetCore.Mvc;

namespace CashFlow.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class LoginController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisteredUser), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseError), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Register(
            [FromServices] ILoginValidation validation,
            [FromBody] RequestLogin request)
    {
        var response = await validation.Execute(request);

        return Created(string.Empty, response);
    }
}

