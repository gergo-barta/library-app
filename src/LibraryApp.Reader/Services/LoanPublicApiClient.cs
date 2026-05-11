using LibraryApp.Shared.DTOs;
using System.Net.Http.Json;

namespace LibraryApp.Reader.Services;

public class LoanPublicApiClient : ILoanPublicApiClient
{
    private readonly HttpClient _http;

    public LoanPublicApiClient(IHttpClientFactory factory) => _http = factory.CreateClient("LibraryApi");

    public async Task<IReadOnlyList<LoanDTO>> GetByReaderAsync(int readerNumber) =>
        await _http.GetFromJsonAsync<List<LoanDTO>>($"api/loans/reader/{readerNumber}") ?? new();
}
