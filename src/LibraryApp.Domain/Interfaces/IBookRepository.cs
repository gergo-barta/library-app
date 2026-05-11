using LibraryApp.Domain.Entities;

namespace LibraryApp.Domain.Interfaces;

public interface IBookRepository
{
    Task<IReadOnlyList<Book>> GetAllAsync();
    Task<Book?> GetByIdAsync(int inventoryNumber);
    Task<IReadOnlyList<Book>> GetAvailableAsync();
    Task<Book> CreateAsync(Book book);
    Task UpdateAsync(Book book);
    Task DeleteAsync(int inventoryNumber);
}
