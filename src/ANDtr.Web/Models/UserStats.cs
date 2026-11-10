namespace ANDtr.Web.Models;

// Counts for the Users page. Deleted users are left out of every number.
public class UserStats
{
    public const int DormantDays = 90;

    public int Total { get; set; }

    public int Active { get; set; }

    public int Inactive { get; set; }

    public int Locked { get; set; }

    // Active users who have not logged in for DormantDays (or never have).
    public int Dormant { get; set; }

    // Every role in Roles.All order, including roles with no users.
    public Dictionary<string, int> ByRole { get; set; } = [];

    // Organisations with the most customer users, largest first.
    public List<OrganisationCount> TopOrganisations { get; set; } = [];
}

public record OrganisationCount(int Id, string Name, int Customers);
