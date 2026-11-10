using System.Text;
using ANDtr.Web.Helpers;

namespace ANDtr.Tests;

public sealed class CsvTests
{
    [Fact]
    public void Build_QuotesCommasAndQuotesAndNeutralisesFormulas()
    {
        var bytes = Csv.Build(["Name", "Note"], [["Iyer, Ananya", "He said \"hi\""], ["=HYPERLINK(\"x\")", null]]);

        Assert.Equal(Encoding.UTF8.GetPreamble(), bytes.Take(3));
        var text = Encoding.UTF8.GetString(bytes[3..]);
        Assert.Equal(
            "Name,Note\r\n" +
            "\"Iyer, Ananya\",\"He said \"\"hi\"\"\"\r\n" +
            "\"'=HYPERLINK(\"\"x\"\")\",\r\n",
            text);
    }
}
