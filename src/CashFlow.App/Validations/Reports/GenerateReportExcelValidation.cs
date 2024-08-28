using CashFlow.Domain.Repos.Expenses;
using ClosedXML.Excel;

namespace CashFlow.App.Validations.Reports;
public class GenerateReportExcelValidation : IGenerateReportExcelValidation
{
    private readonly IExpenseReadOnly _repos;
    public GenerateReportExcelValidation(IExpenseReadOnly repos)
    {
        _repos = repos;
    }
    public async Task<byte[]> Execute(DateOnly month)
    {
        var expenses = await _repos.FilterByMonth(month);
        var workbook = new XLWorkbook();

        workbook.Author= "Marcos";
        workbook.Style.Font.FontSize = 12;
        workbook.Style.Font.FontName = "Arial";
 
        var worksheet = workbook.Worksheets.Add(month.ToString("Y"));
        InsertHeader(worksheet);
        var file = new MemoryStream();
        workbook.SaveAs(file);

        return file.ToArray();
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
