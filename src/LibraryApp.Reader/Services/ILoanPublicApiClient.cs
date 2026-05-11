using LibraryApp.Shared.DTOs;

namespace LibraryApp.Reader.Services;

public interface ILoanPublicApiClient
{
    Task<IReadOnlyList<LoanDTO>> GetByReaderAsync(int readerNumber);
}
