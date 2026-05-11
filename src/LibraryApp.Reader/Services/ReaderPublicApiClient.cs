using LibraryApp.Shared.DTOs;
using System.Net;
using System.Net.Http.Json;

namespace LibraryApp.Reader.Services;

public class ReaderPublicApiClient : IReaderPublicApiClient
{
    private readonly HttpClient _http;

    public ReaderPublicApiClient(IHttpClientFactory factory) => _http = factory.CreateClient("LibraryApi");

    public async Task<ReaderDTO?> GetByNumberAsync(int readerNumber)
    {
        var response = await _http.GetAsync($"api/readers/{readerNumber}");
        if (response.StatusCode == HttpStatusCode.NotFound) return null;
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<ReaderDTO>();
    }
}
