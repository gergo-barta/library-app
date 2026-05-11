using LibraryApp.Shared.DTOs;
using LibraryApp.Shared.Requests;
using System.Net.Http.Json;

namespace LibraryApp.Reader.Services;

public class ReviewPublicApiClient : IReviewPublicApiClient
{
    private readonly HttpClient _http;

    public ReviewPublicApiClient(IHttpClientFactory factory) => _http = factory.CreateClient("LibraryApi");

    public async Task<IReadOnlyList<ReviewDTO>> GetByBookAsync(int inventoryNumber) =>
        await _http.GetFromJsonAsync<List<ReviewDTO>>($"api/reviews/book/{inventoryNumber}") ?? new();

    public Task<HttpResponseMessage> CreateAsync(CreateReviewRequest request) =>
        _http.PostAsJsonAsync("api/reviews", request);
}
