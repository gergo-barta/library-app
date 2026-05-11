using LibraryApp.Shared.DTOs;
using LibraryApp.Shared.Requests;
using System.Net.Http.Json;

namespace LibraryApp.Admin.Services;

public class LoanApiClient : ILoanApiClient
{
    private readonly HttpClient _http;

    public LoanApiClient(IHttpClientFactory factory) => _http = factory.CreateClient("LibraryApi");

    public async Task<IReadOnlyList<LoanDTO>> GetAllAsync() =>
        await _http.GetFromJsonAsync<List<LoanDTO>>("api/loans") ?? new();

    public async Task<LoanDTO?> GetByIdAsync(int id) =>
        await _http.GetFromJsonAsync<LoanDTO>($"api/loans/{id}");

    public Task<HttpResponseMessage> CreateAsync(CreateLoanRequest request) =>
        _http.PostAsJsonAsync("api/loans", request);

    public Task<HttpResponseMessage> ReturnAsync(int id) =>
        _http.PutAsync($"api/loans/{id}/return", null);

    public Task<HttpResponseMessage> DeleteAsync(int id) =>
        _http.DeleteAsync($"api/loans/{id}");
}
