namespace CashFlow.App.Validations.Reports.PDF;
public interface IGenerateReportPDFValidation
{
    Task<byte[]> Execute(DateOnly month);
}
