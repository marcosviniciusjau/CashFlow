using CashFlow.App.Validations.Reports.PDF.Colors;
using CashFlow.App.Validations.Reports.PDF.Fonts;
using CashFlow.Domain.Entities.Enums;
using CashFlow.Domain.Extensions;
using CashFlow.Domain.Repos.Expenses;
using CashFlow.Domain.Services;
using ClosedXML.Excel;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;
using PdfSharp.Fonts;
using System.Reflection;
using Cell = MigraDoc.DocumentObjectModel.Tables.Cell;
using Font = MigraDoc.DocumentObjectModel.Font;
using Section = MigraDoc.DocumentObjectModel.Section;
using Table = MigraDoc.DocumentObjectModel.Tables.Table;

namespace CashFlow.App.Validations.Reports.PDF;
public class GenerateReportPDFValidation : IGenerateReportPDFValidation
{
    private readonly IExpenseReadOnly _repos;
    private const string CURRENCY = "R$";
    private readonly ILoggedUser _loggedUser;
    public GenerateReportPDFValidation(IExpenseReadOnly repos, ILoggedUser loggedUser)
    {
        _repos = repos;
        _loggedUser = loggedUser;
        GlobalFontSettings.FontResolver = new ExpensesFonts();
    }
    public async Task<byte[]> Execute(DateOnly month)
    {
        var loggedUser = await _loggedUser.Get();
        var expenses = await _repos.FilterByMonth(loggedUser,month);
        if (expenses.Count == 0)
        {
            return [];
        }

        var document = CreateDocument(loggedUser.ManagerName, month);
        var page = CreatePage(document);

        CreateHeader(loggedUser.ManagerName, page);
        var paragraph = page.AddParagraph(); 
        var totalExpenses = expenses.Sum(expense => expense.Amount);

        CreateTotalSpentSection(page, month,totalExpenses);
      
        foreach(var expense in expenses)
        {
            var table = CreateExpenseTable(page);
            var row = table.AddRow();
            row.Height = 25;
            AddExpenseTitle(row.Cells[0], expense.Title);
            AddExpenseAmountHeader(row.Cells[3]);

            row = table.AddRow();
            row.Height = 25;

            row.Cells[0].AddParagraph(expense.Date.ToString("D"));
            SetStyles(row.Cells[0]);
            row.Cells[0].Format.LeftIndent = 20;

            row.Cells[1].AddParagraph(expense.Date.ToString("t"));
            SetStyles(row.Cells[1]);
            
            row.Cells[2].AddParagraph(expense.PaymentType.PaymentTypeToString());
            SetStyles(row.Cells[2]);

            AddExpenseAmount(row.Cells[3], expense.Amount);

            if (string.IsNullOrWhiteSpace(expense.Description) == false)
            {
                var descriptionRow = table.AddRow();
                descriptionRow.Height = 25;

                descriptionRow.Cells[0].AddParagraph(expense.Description);
                descriptionRow.Cells[0].Format.Font = new Font { Name = FontHelper.WORKSANS_REGULAR, Size = 10, Color = ColorsHelper.BLACK };
                descriptionRow.Cells[0].Shading = new Shading { Color = ColorsHelper.GREEN_LIGHT };
                descriptionRow.Cells[0].VerticalAlignment = VerticalAlignment.Center;
                descriptionRow.Cells[0].MergeRight = 2;
                descriptionRow.Cells[0].Format.LeftIndent = 20;

                row.Cells[3].MergeDown = 1;
            }
            row = table.AddRow();
            row.Height = 30;
            row.Borders.Visible = false;
        }
        return RenderDocument(document);
    }

    private Document CreateDocument(string author, DateOnly month)
    {
        var document = new Document();
        document.Info.Title =  $"{"Expenses for"} {month:Y}";
        document.Info.Author = author;

        var style = document.Styles["Normal"];
        style!.Font.Name = FontHelper.RALEWAY_REGULAR;

        return document;
    }

    private static void CreateHeader(string name,Section page)
    {
        var table = page.AddTable();
        table.AddColumn();
        table.AddColumn("300");

        var row = table.AddRow();

        var assembly = Assembly.GetExecutingAssembly();
        var directoryName = Path.GetDirectoryName(assembly.Location);
        var pathFile = Path.Combine(directoryName!, "Logo", "ProfilePhoto.png");

        row.Cells[0].AddImage(pathFile);

        row.Cells[1].AddParagraph($"Olá {name}");
        row.Cells[1].Format.Font = new Font { Name = FontHelper.RALEWAY_BLACK, Size = 16 };
        row.Cells[1].VerticalAlignment = VerticalAlignment.Center;
    }

    private Section CreatePage(Document document)
    {
        var section = document.AddSection();
        section.PageSetup = document.DefaultPageSetup.Clone();

        section.PageSetup.PageFormat = PageFormat.A4;

        section.PageSetup.LeftMargin = 40;
        section.PageSetup.RightMargin = 40;
        section.PageSetup.TopMargin = 80;
        section.PageSetup.BottomMargin = 80;
        return section;
    }

    private byte[] RenderDocument(Document document)
    {
        var renderer = new PdfDocumentRenderer
        {
            Document = document,
        };

        renderer.RenderDocument();
        using var file = new MemoryStream();
        renderer.PdfDocument.Save(file);

        return file.ToArray();
    }

    private void CreateTotalSpentSection(Section page, DateOnly month, decimal totalExpenses)
    {
        var paragraph = page.AddParagraph();
        paragraph.Format.SpaceBefore = "40";
        paragraph.Format.SpaceAfter = "40";
        var title = string.Format("Total spent in {0}", month.ToString("Y"));
        paragraph.AddFormattedText(title, new Font { Name = FontHelper.RALEWAY_REGULAR, Size = 15 });
        paragraph.AddLineBreak();

        paragraph.AddFormattedText($"{CURRENCY} {totalExpenses:f2}", new Font { Name = FontHelper.WORKSANS_BLACK, Size = 50 });
    }
    private Table CreateExpenseTable(Section page)
    {
        var table = page.AddTable();
        table.AddColumn("195").Format.Alignment = ParagraphAlignment.Left;
        table.AddColumn("80").Format.Alignment = ParagraphAlignment.Center; ;
        table.AddColumn("128").Format.Alignment = ParagraphAlignment.Center; ;
        table.AddColumn("128").Format.Alignment = ParagraphAlignment.Right; ;
        return table;
    }

    private void AddExpenseTitle(Cell cell,string expenseTitle)
    {
        cell.AddParagraph(expenseTitle);
        cell.Format.Font = new Font { Name = FontHelper.RALEWAY_BLACK, Size = 14, Color = ColorsHelper.BLACK };
        cell.Shading = new Shading { Color = ColorsHelper.RED_LIGHT };
        cell.VerticalAlignment = VerticalAlignment.Center;
        cell.MergeRight = 2;
        cell.Format.LeftIndent = 20;
    }
    private void AddExpenseAmountHeader(Cell cell)
    {
        cell.AddParagraph("Amount");
        cell.Format.Font = new Font { Name = FontHelper.RALEWAY_BLACK, Size = 14, Color = ColorsHelper.WHITE };
        cell.Shading = new Shading { Color = ColorsHelper.RED_DARK };
        cell.VerticalAlignment = VerticalAlignment.Center;

    }

    private void SetStyles(Cell cell)
    {
        cell.Format.Font = new Font { Name = FontHelper.WORKSANS_REGULAR, Size = 12, Color = ColorsHelper.BLACK };
        cell.Shading.Color = ColorsHelper.GREEN_DARK;
        cell.VerticalAlignment = VerticalAlignment.Center;
        cell.Format.LeftIndent = 20;
    }

    private void AddExpenseAmount(Cell cell,decimal amount)
    {
        cell.AddParagraph($"-{CURRENCY} {amount:f2}");

        cell.Format.Font = new Font { Name = FontHelper.WORKSANS_REGULAR, Size = 14, Color = ColorsHelper.BLACK };
        cell.Shading.Color = ColorsHelper.WHITE;
        cell.VerticalAlignment = VerticalAlignment.Center;
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
