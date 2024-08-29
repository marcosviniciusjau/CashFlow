namespace CashFlow.App.Validations.Reports.Excel;
public interface IGenerateReportExcelValidation
{
    Task<byte[]> Execute(DateOnly month);
}
