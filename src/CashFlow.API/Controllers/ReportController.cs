using CashFlow.App.Validations.Reports.Excel;
using CashFlow.App.Validations.Reports.PDF;
using CashFlow.Domain.Entities.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace CashFlow.API.Controllers;
[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = Roles.ADMIN)]
public class ReportController : ControllerBase
{
    [HttpGet("excel")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetExcel(
        [FromServices] IGenerateReportExcelValidation validation,
        [FromHeader] DateOnly month)
    {
        byte [] file = await validation.Execute(month);
        if (file.Length > 0)
        {
           return File(file, MediaTypeNames.Application.Octet, "report.xlsx");
        }
         return NoContent();
    }  
    [HttpGet("pdf")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetPDF(
        [FromServices] IGenerateReportPDFValidation validation,
        [FromHeader] DateOnly month)
    {
        byte [] file = await validation.Execute(month);
        if (file.Length > 0)
        {
            return File(file, MediaTypeNames.Application.Pdf, "report.pdf");
        }
        return NoContent();
    }
}
