using LibraryApp.Domain.Entities;
using LibraryApp.Domain.Interfaces;
using LibraryApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Infrastructure.Repositories;

public class ReviewRepository : IReviewRepository
{
    private readonly LibraryDbContext _db;

    public ReviewRepository(LibraryDbContext db) => _db = db;

    public async Task<IReadOnlyList<Review>> GetAllAsync() =>
        await _db.Reviews
            .Include(r => r.Book)
            .Include(r => r.Reader)
            .AsNoTracking()
            .OrderByDescending(r => r.CreatedAt)
            .ToListAsync();

    public async Task<Review?> GetByIdAsync(int id) =>
        await _db.Reviews
            .Include(r => r.Book)
            .Include(r => r.Reader)
            .FirstOrDefaultAsync(r => r.Id == id);

    public async Task<IReadOnlyList<Review>> GetByBookAsync(int inventoryNumber) =>
        await _db.Reviews
            .Include(r => r.Reader)
            .Where(r => r.InventoryNumber == inventoryNumber)
            .AsNoTracking()
            .OrderByDescending(r => r.CreatedAt)
            .ToListAsync();

    public async Task<Review> CreateAsync(Review review)
    {
        _db.Reviews.Add(review);
        await _db.SaveChangesAsync();
        return review;
    }

    public async Task UpdateAsync(Review review)
    {
        _db.Reviews.Update(review);
        await _db.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await _db.Reviews.FindAsync(id);
        if (entity is null) return;
        _db.Reviews.Remove(entity);
        await _db.SaveChangesAsync();
    }
}
