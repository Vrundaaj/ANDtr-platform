using System.Text;

namespace ANDtr.Web.Helpers;

public static class Csv
{
    // UTF-8 with a byte order mark, so Excel shows accented names correctly.
    public static byte[] Build(IEnumerable<string> headers, IEnumerable<IEnumerable<string?>> rows)
    {
        var builder = new StringBuilder();
        AppendRow(builder, headers);
        foreach (var row in rows)
        {
            AppendRow(builder, row);
        }

        return Encoding.UTF8.GetPreamble().Concat(Encoding.UTF8.GetBytes(builder.ToString())).ToArray();
    }

    private static void AppendRow(StringBuilder builder, IEnumerable<string?> cells)
    {
        builder.AppendJoin(',', cells.Select(Escape));
        builder.Append("\r\n");
    }

    private static string Escape(string? value)
    {
        value ??= string.Empty;

        // A cell starting with = + - @ would run as a formula when opened in Excel.
        if (value.Length > 0 && "=+-@\t\r".Contains(value[0]))
        {
            value = "'" + value;
        }

        return value.IndexOfAny([',', '"', '\r', '\n']) >= 0
            ? $"\"{value.Replace("\"", "\"\"")}\""
            : value;
    }
}
