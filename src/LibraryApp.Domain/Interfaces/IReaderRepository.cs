using LibraryApp.Domain.Entities;

namespace LibraryApp.Domain.Interfaces;

public interface IReaderRepository
{
    Task<IReadOnlyList<Reader>> GetAllAsync();
    Task<Reader?> GetByIdAsync(int readerNumber);
    Task<Reader> CreateAsync(Reader reader);
    Task UpdateAsync(Reader reader);
    Task DeleteAsync(int readerNumber);
}
