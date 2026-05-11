using Blazored.LocalStorage;
using LibraryApp.Shared.Requests;
using System.Net.Http.Json;

namespace LibraryApp.Admin.Auth;

public class AdminAuthService
{
    public const string PasswordStorageKey = "library_admin_password";

    private readonly IHttpClientFactory _httpFactory;
    private readonly ILocalStorageService _localStorage;

    public AdminAuthService(IHttpClientFactory httpFactory, ILocalStorageService localStorage)
    {
        _httpFactory = httpFactory;
        _localStorage = localStorage;
    }

    public async Task<bool> LoginAsync(string password)
    {
        if (string.IsNullOrWhiteSpace(password)) return false;
        var client = _httpFactory.CreateClient("LibraryApi");
        var response = await client.PostAsJsonAsync("api/auth/verify", new LoginRequest { Password = password });
        if (!response.IsSuccessStatusCode) return false;
        await _localStorage.SetItemAsStringAsync(PasswordStorageKey, password);
        return true;
    }

    public async Task LogoutAsync()
    {
        await _localStorage.RemoveItemAsync(PasswordStorageKey);
    }

    public async Task<string?> GetPasswordAsync()
    {
        return await _localStorage.GetItemAsStringAsync(PasswordStorageKey);
    }

    public async Task<bool> IsLoggedInAsync()
    {
        var pwd = await GetPasswordAsync();
        return !string.IsNullOrWhiteSpace(pwd);
    }
}
