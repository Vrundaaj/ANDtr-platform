namespace ANDtr.Web.Models;

public class UserListItem
{
    public string Id { get; set; } = string.Empty;

    public string FullName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string? Role { get; set; }

    public string? Organisation { get; set; }

    public bool IsActive { get; set; }

    public DateTime? LastLoginAt { get; set; }

    public DateTimeOffset? LockoutEnd { get; set; }

    // Locked by Identity after too many wrong passwords.
    public bool IsLocked => LockoutEnd > DateTimeOffset.UtcNow;

    public DateTime? DeletedAt { get; set; }

    public bool IsDeleted => DeletedAt is not null;
}

public class UserListViewModel
{
    public string? Search { get; set; }

    public string? Role { get; set; }

    public bool? IsActive { get; set; }

    // Shows only deleted users; otherwise deleted users are hidden.
    public bool Deleted { get; set; }

    // Shows only users locked out by Identity after too many wrong passwords.
    public bool Locked { get; set; }

    // Shows only active users with no login in UserStats.DormantDays days.
    public bool Dormant { get; set; }

    // The Status dropdown, mapped onto IsActive, Deleted, Locked and Dormant.
    public string? Status
    {
        get => Deleted ? UserStatus.Deleted
            : Locked ? UserStatus.Locked
            : Dormant ? UserStatus.Dormant
            : IsActive switch { true => UserStatus.Active, false => UserStatus.Inactive, null => null };
        set
        {
            Deleted = value == UserStatus.Deleted;
            Locked = value == UserStatus.Locked;
            Dormant = value == UserStatus.Dormant;
            IsActive = value switch
            {
                UserStatus.Active or UserStatus.Dormant => true,
                UserStatus.Inactive => false,
                _ => null,
            };
        }
    }

    public int? OrganisationId { get; set; }

    public string? OrganisationName { get; set; }

    // Headline numbers and charts at the top of the page. Ignores the filters above.
    public UserStats Stats { get; set; } = new();

    // Column key from UserSort; empty means name.
    public string? Sort { get; set; }

    public bool Desc { get; set; }

    public int Page { get; set; } = 1;

    public int PageSize { get; set; }

    public int TotalCount { get; set; }

    public int TotalPages => Math.Max(1, (int)Math.Ceiling(TotalCount / (double)PageSize));

    public List<UserListItem> Users { get; set; } = [];
}

public static class UserStatus
{
    public const string Active = "active";
    public const string Dormant = "dormant";
    public const string Inactive = "inactive";
    public const string Locked = "locked";
    public const string Deleted = "deleted";
}

public static class UserSort
{
    public const string Name = "name";
    public const string Email = "email";
    public const string Role = "role";
    public const string Organisation = "organisation";
    public const string Status = "status";
    public const string LastLogin = "lastlogin";
}
