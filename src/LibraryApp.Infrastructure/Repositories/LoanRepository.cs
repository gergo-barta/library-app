using LibraryApp.Domain.Entities;
using LibraryApp.Domain.Interfaces;
using LibraryApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Infrastructure.Repositories;

public class LoanRepository : ILoanRepository
{
    private readonly LibraryDbContext _db;

    public LoanRepository(LibraryDbContext db) => _db = db;

    public async Task<IReadOnlyList<Loan>> GetAllAsync() =>
        await _db.Loans
            .Include(l => l.Reader)
            .Include(l => l.Book)
            .AsNoTracking()
            .OrderByDescending(l => l.LoanDate)
            .ToListAsync();

    public async Task<Loan?> GetByIdAsync(int id) =>
        await _db.Loans
            .Include(l => l.Reader)
            .Include(l => l.Book)
            .FirstOrDefaultAsync(l => l.Id == id);

    public async Task<IReadOnlyList<Loan>> GetByReaderAsync(int readerNumber) =>
        await _db.Loans
            .Include(l => l.Reader)
            .Include(l => l.Book)
            .Where(l => l.ReaderNumber == readerNumber)
            .AsNoTracking()
            .OrderByDescending(l => l.LoanDate)
            .ToListAsync();

    public async Task<Loan> CreateAsync(Loan loan)
    {
        _db.Loans.Add(loan);
        await _db.SaveChangesAsync();
        return loan;
    }

    public async Task UpdateAsync(Loan loan)
    {
        _db.Loans.Update(loan);
        await _db.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await _db.Loans.FindAsync(id);
        if (entity is null) return;
        _db.Loans.Remove(entity);
        await _db.SaveChangesAsync();
    }

    public async Task<bool> IsBookCurrentlyLoanedAsync(int inventoryNumber) =>
        await _db.Loans.AnyAsync(l => l.InventoryNumber == inventoryNumber && l.ReturnDate == null);
}
