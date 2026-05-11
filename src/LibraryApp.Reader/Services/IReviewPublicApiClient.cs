using LibraryApp.Shared.DTOs;
using LibraryApp.Shared.Requests;

namespace LibraryApp.Reader.Services;

public interface IReviewPublicApiClient
{
    Task<IReadOnlyList<ReviewDTO>> GetByBookAsync(int inventoryNumber);
    Task<HttpResponseMessage> CreateAsync(CreateReviewRequest request);
}
