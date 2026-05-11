using LibraryApp.Shared.DTOs;
using LibraryApp.Shared.Requests;

namespace LibraryApp.Application.Interfaces;

public interface IReviewService
{
    Task<IReadOnlyList<ReviewDTO>> GetByBookAsync(int inventoryNumber);
    Task<ReviewDTO?> AddAsync(CreateReviewRequest request);
    Task<bool> DeleteAsync(int id);
}
