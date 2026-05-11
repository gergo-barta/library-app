using LibraryApp.Shared.DTOs;
using LibraryApp.Shared.Requests;

namespace LibraryApp.Admin.Services;

public interface IBookApiClient
{
    Task<IReadOnlyList<BookDTO>> GetAllAsync();
    Task<BookDTO?> GetByIdAsync(int id);
    Task<IReadOnlyList<BookDTO>> GetAvailableAsync();
    Task<HttpResponseMessage> CreateAsync(CreateBookRequest request);
    Task<HttpResponseMessage> UpdateAsync(int id, UpdateBookRequest request);
    Task<HttpResponseMessage> DeleteAsync(int id);
}
