namespace LibraryApp.API.Auth;

public class AdminPasswordOptions
{
    public const string SectionName = "Admin";
    public string PasswordHash { get; set; } = string.Empty;
}
