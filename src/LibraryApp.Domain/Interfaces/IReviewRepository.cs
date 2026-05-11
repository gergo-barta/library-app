using LibraryApp.Domain.Entities;

namespace LibraryApp.Domain.Interfaces;

public interface IReviewRepository
{
    Task<IReadOnlyList<Review>> GetAllAsync();
    Task<Review?> GetByIdAsync(int id);
    Task<IReadOnlyList<Review>> GetByBookAsync(int inventoryNumber);
    Task<Review> CreateAsync(Review review);
    Task UpdateAsync(Review review);
    Task DeleteAsync(int id);
}
