using LibraryApp.Shared.DTOs;
using LibraryApp.Shared.Requests;

namespace LibraryApp.Application.Interfaces;

public interface ILoanService
{
    Task<IReadOnlyList<LoanDTO>> GetAllAsync();
    Task<LoanDTO?> GetByIdAsync(int id);
    Task<IReadOnlyList<LoanDTO>> GetByReaderAsync(int readerNumber);
    Task<LoanDTO?> CreateAsync(CreateLoanRequest request);
    Task<bool> DeleteAsync(int id);
    Task<LoanDTO?> ReturnBookAsync(int loanId);
}
