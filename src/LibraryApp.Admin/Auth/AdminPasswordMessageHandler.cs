using Blazored.LocalStorage;

namespace LibraryApp.Admin.Auth;

public class AdminPasswordMessageHandler : DelegatingHandler
{
    public const string HeaderName = "X-Admin-Password";

    private readonly ILocalStorageService _localStorage;

    public AdminPasswordMessageHandler(ILocalStorageService localStorage)
    {
        _localStorage = localStorage;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var password = await _localStorage.GetItemAsStringAsync(AdminAuthService.PasswordStorageKey, cancellationToken);
        if (!string.IsNullOrWhiteSpace(password))
        {
            request.Headers.Remove(HeaderName);
            request.Headers.Add(HeaderName, password);
        }
        return await base.SendAsync(request, cancellationToken);
    }
}
