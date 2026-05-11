namespace LibraryApp.Application.Interfaces;

public interface IAuthService
{
    bool VerifyAdminPassword(string password);
}
