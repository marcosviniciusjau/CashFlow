using MigraDoc.DocumentObjectModel;
using PdfSharp.Fonts;
using System.Reflection;

namespace CashFlow.App.Validations.Reports.PDF.Fonts;
public class ExpensesFonts : IFontResolver
{
    public byte[]? GetFont(string faceName)
    {
        var stream = ReadFontFile(faceName);
        if (stream is null)
        {
            stream = ReadFontFile(FontHelper.DEFAULT_FONT);
        }
        var lenght = (int)stream!.Length;
        var data = new byte[lenght];
        stream!.Read(buffer: data, offset: 0, count: lenght);
        return data;
    }

    public FontResolverInfo? ResolveTypeface(string familyName, bool bold, bool italic)
    {
        return new FontResolverInfo(familyName);
    }

    private Stream? ReadFontFile(string faceName)
    {
        var assembly = Assembly.GetExecutingAssembly();
        return assembly.GetManifestResourceStream($"CashFlow.App.Validations.Reports.PDF.Fonts.{faceName}.ttf");
    }
}
