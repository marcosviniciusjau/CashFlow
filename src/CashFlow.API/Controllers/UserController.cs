using CashFlow.App.Validations.Users.ChangePassword;
using CashFlow.App.Validations.Users.Delete;
using CashFlow.App.Validations.Users.GetProfile;
using CashFlow.App.Validations.Users.Register;
using CashFlow.App.Validations.Users.Update;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CashFlow.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(ResponseRegisteredUser), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseError), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register(
            [FromServices] IRegisterUserValidation validation,
            [FromBody] RequestUser request)
        {
            var response = await validation.Execute(request);

            return Created(string.Empty, response);
        }

        [HttpGet]
        [ProducesResponseType(typeof(ResponseUserProfile), StatusCodes.Status200OK)]
        [Authorize]
        public async Task<IActionResult> GetProfile([FromServices] IGetProfileValidation validation)
        {
            var response = await validation.Execute();

            return Ok(response);
        }

        [HttpPut]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseError), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateProfile(
            [FromServices] IUpdateProfileValidation validation,
            [FromBody] RequestUpdateUser request)
        {
            await validation.Execute(request);

            return NoContent();
        }

        [HttpPut("change-password")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseError), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ChangePassword(
                [FromServices] IChangePasswordValidation validation,
                [FromBody] RequestChangePassword request)
        {
            await validation.Execute(request);

            return NoContent();
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [Authorize]
        public async Task<IActionResult> DeleteProfile([FromServices] IDeleteProfileValidation validation)
        {
            await validation.Execute();

            return NoContent();
        }
    }
}