using LibraryApp.Domain.Entities;
using LibraryApp.Domain.Interfaces;
using LibraryApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Infrastructure.Repositories;

public class ReaderRepository : IReaderRepository
{
    private readonly LibraryDbContext _db;

    public ReaderRepository(LibraryDbContext db) => _db = db;

    public async Task<IReadOnlyList<Reader>> GetAllAsync() =>
        await _db.Readers.AsNoTracking().OrderBy(r => r.ReaderNumber).ToListAsync();

    public async Task<Reader?> GetByIdAsync(int readerNumber) =>
        await _db.Readers.FirstOrDefaultAsync(r => r.ReaderNumber == readerNumber);

    public async Task<Reader> CreateAsync(Reader reader)
    {
        _db.Readers.Add(reader);
        await _db.SaveChangesAsync();
        return reader;
    }

    public async Task UpdateAsync(Reader reader)
    {
        _db.Readers.Update(reader);
        await _db.SaveChangesAsync();
    }

    public async Task DeleteAsync(int readerNumber)
    {
        var entity = await _db.Readers.FindAsync(readerNumber);
        if (entity is null) return;
        _db.Readers.Remove(entity);
        await _db.SaveChangesAsync();
    }
}
