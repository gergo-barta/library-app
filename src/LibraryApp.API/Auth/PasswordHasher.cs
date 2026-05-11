using LibraryApp.Application.Services;
using Microsoft.Extensions.Options;

namespace LibraryApp.API.Auth;

public class PasswordHasher : IPasswordVerifier
{
    public bool Verify(string password, string hash)
    {
        if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(hash)) return false;
        try
        {
            return BCrypt.Net.BCrypt.Verify(password, hash);
        }
        catch
        {
            return false;
        }
    }
}

public class OptionsAdminPasswordProvider : IAdminPasswordProvider
{
    private readonly IOptionsMonitor<AdminPasswordOptions> _options;

    public OptionsAdminPasswordProvider(IOptionsMonitor<AdminPasswordOptions> options)
    {
        _options = options;
    }

    public string GetHash() => _options.CurrentValue.PasswordHash ?? string.Empty;
}
