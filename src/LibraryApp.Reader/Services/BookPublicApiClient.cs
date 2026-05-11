using LibraryApp.Shared.DTOs;
using System.Net.Http.Json;

namespace LibraryApp.Reader.Services;

public class BookPublicApiClient : IBookPublicApiClient
{
    private readonly HttpClient _http;

    public BookPublicApiClient(IHttpClientFactory factory) => _http = factory.CreateClient("LibraryApi");

    public async Task<IReadOnlyList<BookDTO>> GetAvailableAsync() =>
        await _http.GetFromJsonAsync<List<BookDTO>>("api/books/available") ?? new();

    public async Task<BookDTO?> GetByIdAsync(int inventoryNumber) =>
        await _http.GetFromJsonAsync<BookDTO>($"api/books/{inventoryNumber}");
}
