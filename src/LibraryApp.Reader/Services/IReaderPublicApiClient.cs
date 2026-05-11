using LibraryApp.Shared.DTOs;

namespace LibraryApp.Reader.Services;

public interface IReaderPublicApiClient
{
    Task<ReaderDTO?> GetByNumberAsync(int readerNumber);
}
