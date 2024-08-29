using CashFlow.App.Validations.Reports.PDF.Fonts;
using CashFlow.Domain.Entities.Enums;
using CashFlow.Domain.Repos.Expenses;
using ClosedXML.Excel;
using PdfSharp.Fonts;

namespace CashFlow.App.Validations.Reports.PDF;
public class GenerateReportPDFValidation : IGenerateReportPDFValidation
{
    private readonly IExpenseReadOnly _repos;
    private const string CURRENCY = "R$";
    public GenerateReportPDFValidation(IExpenseReadOnly repos)
    {
        _repos = repos;
        GlobalFontSettings.FontResolver = new ExpensesFonts();
    }
    public async Task<byte[]> Execute(DateOnly month)
    {
        var expenses = await _repos.FilterByMonth(month);
        if (expenses.Count == 0)
        {
            return [];
        }

        return [];
    }
    private string ConvertPaymentType(PaymentTypes payment)
    {
        return payment switch
        {
            PaymentTypes.Cash => "Dinheiro",
            PaymentTypes.CreditCard => "Cartão crédito",
            PaymentTypes.DebitCard => "Cartão débito",
            PaymentTypes.EletronicTransfer => "TED",
            _ => string.Empty
        };
    }

    private void InsertHeader(IXLWorksheet worksheet)
    {
        worksheet.Cell("A1").Value = "Title";
        worksheet.Cell("B1").Value = "Date";
        worksheet.Cell("C1").Value = "Payment Type";
        worksheet.Cell("D1").Value = "Amount";
        worksheet.Cell("E1").Value = "Description";

        worksheet.Cells("A1:E1").Style.Font.Bold = true;
        worksheet.Cells("A1:E1").Style.Fill.BackgroundColor = XLColor.Yellow;

        worksheet.Cell("A1").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
        worksheet.Cell("B1").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
        worksheet.Cell("C1").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
        worksheet.Cell("E1").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
        worksheet.Cell("D1").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;


    }
}
