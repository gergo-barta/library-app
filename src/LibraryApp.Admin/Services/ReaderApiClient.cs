using LibraryApp.Shared.DTOs;
using LibraryApp.Shared.Requests;
using System.Net.Http.Json;

namespace LibraryApp.Admin.Services;

public class ReaderApiClient : IReaderApiClient
{
    private readonly HttpClient _http;

    public ReaderApiClient(IHttpClientFactory factory) => _http = factory.CreateClient("LibraryApi");

    public async Task<IReadOnlyList<ReaderDTO>> GetAllAsync() =>
        await _http.GetFromJsonAsync<List<ReaderDTO>>("api/readers") ?? new();

    public async Task<ReaderDTO?> GetByIdAsync(int readerNumber) =>
        await _http.GetFromJsonAsync<ReaderDTO>($"api/readers/{readerNumber}");

    public Task<HttpResponseMessage> CreateAsync(CreateReaderRequest request) =>
        _http.PostAsJsonAsync("api/readers", request);

    public Task<HttpResponseMessage> UpdateAsync(int readerNumber, UpdateReaderRequest request) =>
        _http.PutAsJsonAsync($"api/readers/{readerNumber}", request);

    public Task<HttpResponseMessage> DeleteAsync(int readerNumber) =>
        _http.DeleteAsync($"api/readers/{readerNumber}");
}
