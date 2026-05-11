using LibraryApp.Domain.Entities;
using LibraryApp.Domain.Interfaces;
using LibraryApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Infrastructure.Repositories;

public class BookRepository : IBookRepository
{
    private readonly LibraryDbContext _db;

    public BookRepository(LibraryDbContext db) => _db = db;

    public async Task<IReadOnlyList<Book>> GetAllAsync() =>
        await _db.Books
            .Include(b => b.Reviews)
            .AsNoTracking()
            .OrderBy(b => b.InventoryNumber)
            .ToListAsync();

    public async Task<Book?> GetByIdAsync(int inventoryNumber) =>
        await _db.Books
            .Include(b => b.Reviews)
            .FirstOrDefaultAsync(b => b.InventoryNumber == inventoryNumber);

    public async Task<IReadOnlyList<Book>> GetAvailableAsync()
    {
        var today = DateTime.Today;
        return await _db.Books
            .Include(b => b.Reviews)
            .Where(b => !b.Loans.Any(l => l.ReturnDate == null))
            .AsNoTracking()
            .OrderBy(b => b.Title)
            .ToListAsync();
    }

    public async Task<Book> CreateAsync(Book book)
    {
        _db.Books.Add(book);
        await _db.SaveChangesAsync();
        return book;
    }

    public async Task UpdateAsync(Book book)
    {
        _db.Books.Update(book);
        await _db.SaveChangesAsync();
    }

    public async Task DeleteAsync(int inventoryNumber)
    {
        var entity = await _db.Books.FindAsync(inventoryNumber);
        if (entity is null) return;
        _db.Books.Remove(entity);
        await _db.SaveChangesAsync();
    }
}
