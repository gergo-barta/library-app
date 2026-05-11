using LibraryApp.Shared.DTOs;
using LibraryApp.Shared.Requests;
using System.Net.Http.Json;

namespace LibraryApp.Admin.Services;

public class BookApiClient : IBookApiClient
{
    private readonly HttpClient _http;

    public BookApiClient(IHttpClientFactory factory) => _http = factory.CreateClient("LibraryApi");

    public async Task<IReadOnlyList<BookDTO>> GetAllAsync() =>
        await _http.GetFromJsonAsync<List<BookDTO>>("api/books") ?? new();

    public async Task<BookDTO?> GetByIdAsync(int id) =>
        await _http.GetFromJsonAsync<BookDTO>($"api/books/{id}");

    public async Task<IReadOnlyList<BookDTO>> GetAvailableAsync() =>
        await _http.GetFromJsonAsync<List<BookDTO>>("api/books/available") ?? new();

    public Task<HttpResponseMessage> CreateAsync(CreateBookRequest request) =>
        _http.PostAsJsonAsync("api/books", request);

    public Task<HttpResponseMessage> UpdateAsync(int id, UpdateBookRequest request) =>
        _http.PutAsJsonAsync($"api/books/{id}", request);

    public Task<HttpResponseMessage> DeleteAsync(int id) =>
        _http.DeleteAsync($"api/books/{id}");
}
