using LibraryApp.Domain.Entities;

namespace LibraryApp.Domain.Interfaces;

public interface ILoanRepository
{
    Task<IReadOnlyList<Loan>> GetAllAsync();
    Task<Loan?> GetByIdAsync(int id);
    Task<IReadOnlyList<Loan>> GetByReaderAsync(int readerNumber);
    Task<Loan> CreateAsync(Loan loan);
    Task UpdateAsync(Loan loan);
    Task DeleteAsync(int id);
    Task<bool> IsBookCurrentlyLoanedAsync(int inventoryNumber);
}
