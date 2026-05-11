using LibraryApp.Shared.DTOs;
using LibraryApp.Shared.Requests;

namespace LibraryApp.Application.Interfaces;

public interface IReaderService
{
    Task<IReadOnlyList<ReaderDTO>> GetAllAsync();
    Task<ReaderDTO?> GetByReaderNumberAsync(int readerNumber);
    Task<ReaderDTO> CreateAsync(CreateReaderRequest request);
    Task<ReaderDTO?> UpdateAsync(int readerNumber, UpdateReaderRequest request);
    Task<bool> DeleteAsync(int readerNumber);
}
