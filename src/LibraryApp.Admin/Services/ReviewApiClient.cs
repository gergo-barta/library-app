using LibraryApp.Shared.DTOs;
using System.Net.Http.Json;

namespace LibraryApp.Admin.Services;

public class ReviewApiClient : IReviewApiClient
{
    private readonly HttpClient _http;

    public ReviewApiClient(IHttpClientFactory factory) => _http = factory.CreateClient("LibraryApi");

    public async Task<IReadOnlyList<ReviewDTO>> GetByBookAsync(int inventoryNumber) =>
        await _http.GetFromJsonAsync<List<ReviewDTO>>($"api/reviews/book/{inventoryNumber}") ?? new();

    public Task<HttpResponseMessage> DeleteAsync(int id) =>
        _http.DeleteAsync($"api/reviews/{id}");
}
