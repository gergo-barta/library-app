using LibraryApp.Shared.DTOs;
using LibraryApp.Shared.Requests;

namespace LibraryApp.Admin.Services;

public interface IReaderApiClient
{
    Task<IReadOnlyList<ReaderDTO>> GetAllAsync();
    Task<ReaderDTO?> GetByIdAsync(int readerNumber);
    Task<HttpResponseMessage> CreateAsync(CreateReaderRequest request);
    Task<HttpResponseMessage> UpdateAsync(int readerNumber, UpdateReaderRequest request);
    Task<HttpResponseMessage> DeleteAsync(int readerNumber);
}
