using LibraryApp.Shared.DTOs;

namespace LibraryApp.Admin.Services;

public interface IReviewApiClient
{
    Task<IReadOnlyList<ReviewDTO>> GetByBookAsync(int inventoryNumber);
    Task<HttpResponseMessage> DeleteAsync(int id);
}
