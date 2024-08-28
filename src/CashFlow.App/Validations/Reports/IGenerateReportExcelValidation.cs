namespace CashFlow.App.Validations.Reports;
public interface IGenerateReportExcelValidation
{
    Task<byte[]> Execute(DateOnly month);
}
