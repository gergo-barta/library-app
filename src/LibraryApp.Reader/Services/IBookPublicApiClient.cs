using LibraryApp.Shared.DTOs;

namespace LibraryApp.Reader.Services;

public interface IBookPublicApiClient
{
    Task<IReadOnlyList<BookDTO>> GetAvailableAsync();
    Task<BookDTO?> GetByIdAsync(int inventoryNumber);
}
