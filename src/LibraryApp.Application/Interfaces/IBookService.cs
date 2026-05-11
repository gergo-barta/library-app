using LibraryApp.Shared.DTOs;
using LibraryApp.Shared.Requests;

namespace LibraryApp.Application.Interfaces;

public interface IBookService
{
    Task<IReadOnlyList<BookDTO>> GetAllAsync();
    Task<BookDTO?> GetByIdAsync(int inventoryNumber);
    Task<IReadOnlyList<BookDTO>> GetAvailableAsync();
    Task<BookDTO> CreateAsync(CreateBookRequest request);
    Task<BookDTO?> UpdateAsync(int inventoryNumber, UpdateBookRequest request);
    Task<bool> DeleteAsync(int inventoryNumber);
}
