using System.Text.RegularExpressions;

internal static class PasswordValidatorHelpers
{

    [GeneratedRegex(@"[A-Z]+")]
    private static partial Regex UpperCaseLetter();
}