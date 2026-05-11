using LibraryApp.Application.Interfaces;

namespace LibraryApp.Application.Services;

public class AuthService : IAuthService
{
    private readonly IPasswordVerifier _verifier;
    private readonly IAdminPasswordProvider _passwordProvider;

    public AuthService(IPasswordVerifier verifier, IAdminPasswordProvider passwordProvider)
    {
        _verifier = verifier;
        _passwordProvider = passwordProvider;
    }

    public bool VerifyAdminPassword(string password)
    {
        if (string.IsNullOrWhiteSpace(password)) return false;
        var hash = _passwordProvider.GetHash();
        if (string.IsNullOrWhiteSpace(hash)) return false;
        return _verifier.Verify(password, hash);
    }
}

public interface IPasswordVerifier
{
    bool Verify(string password, string hash);
}

public interface IAdminPasswordProvider
{
    string GetHash();
}
