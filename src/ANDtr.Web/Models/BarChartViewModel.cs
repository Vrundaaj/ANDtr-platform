namespace ANDtr.Web.Models;

public record BarChartItem(string Label, int Value, string? Url = null);

public class BarChartViewModel
{
    public string Title { get; init; } = string.Empty;

    public string? Subtitle { get; init; }

    // Singular and plural unit for value labels and tooltips, e.g. ("user", "users").
    public (string One, string Many) Unit { get; init; } = ("item", "items");

    public string EmptyText { get; init; } = "Nothing to show yet.";

    // Optional link in the top-right corner, e.g. "View all".
    public string? LinkText { get; init; }

    public string? LinkUrl { get; init; }

    public List<BarChartItem> Items { get; init; } = [];

    public int Max => Items.Count == 0 ? 0 : Items.Max(i => i.Value);

    public string Format(int value) => $"{value} {(value == 1 ? Unit.One : Unit.Many)}";
}
