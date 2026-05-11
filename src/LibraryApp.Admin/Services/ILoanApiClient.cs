using LibraryApp.Shared.DTOs;
using LibraryApp.Shared.Requests;

namespace LibraryApp.Admin.Services;

public interface ILoanApiClient
{
    Task<IReadOnlyList<LoanDTO>> GetAllAsync();
    Task<LoanDTO?> GetByIdAsync(int id);
    Task<HttpResponseMessage> CreateAsync(CreateLoanRequest request);
    Task<HttpResponseMessage> ReturnAsync(int id);
    Task<HttpResponseMessage> DeleteAsync(int id);
}
