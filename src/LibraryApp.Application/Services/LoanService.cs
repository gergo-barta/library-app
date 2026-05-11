using LibraryApp.Application.Interfaces;
using LibraryApp.Application.Mapping;
using LibraryApp.Domain.Interfaces;
using LibraryApp.Shared.DTOs;
using LibraryApp.Shared.Requests;

namespace LibraryApp.Application.Services;

public class LoanService : ILoanService
{
    private readonly ILoanRepository _loanRepository;
    private readonly IBookRepository _bookRepository;
    private readonly IReaderRepository _readerRepository;
    private readonly ILateFeeService _lateFeeService;

    public LoanService(
        ILoanRepository loanRepository,
        IBookRepository bookRepository,
        IReaderRepository readerRepository,
        ILateFeeService lateFeeService)
    {
        _loanRepository = loanRepository;
        _bookRepository = bookRepository;
        _readerRepository = readerRepository;
        _lateFeeService = lateFeeService;
    }

    public async Task<IReadOnlyList<LoanDTO>> GetAllAsync()
    {
        var loans = await _loanRepository.GetAllAsync();
        return loans.Select(l => l.ToDto(_lateFeeService)).ToList();
    }

    public async Task<LoanDTO?> GetByIdAsync(int id)
    {
        var loan = await _loanRepository.GetByIdAsync(id);
        return loan?.ToDto(_lateFeeService);
    }

    public async Task<IReadOnlyList<LoanDTO>> GetByReaderAsync(int readerNumber)
    {
        var loans = await _loanRepository.GetByReaderAsync(readerNumber);
        return loans.Select(l => l.ToDto(_lateFeeService)).ToList();
    }

    public async Task<LoanDTO?> CreateAsync(CreateLoanRequest request)
    {
        var book = await _bookRepository.GetByIdAsync(request.InventoryNumber);
        if (book is null) return null;
        var reader = await _readerRepository.GetByIdAsync(request.ReaderNumber);
        if (reader is null) return null;
        if (await _loanRepository.IsBookCurrentlyLoanedAsync(request.InventoryNumber)) return null;

        var loan = request.ToEntity();
        var saved = await _loanRepository.CreateAsync(loan);
        var fresh = await _loanRepository.GetByIdAsync(saved.Id);
        return fresh!.ToDto(_lateFeeService);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var existing = await _loanRepository.GetByIdAsync(id);
        if (existing is null) return false;
        await _loanRepository.DeleteAsync(id);
        return true;
    }

    public async Task<LoanDTO?> ReturnBookAsync(int loanId)
    {
        var loan = await _loanRepository.GetByIdAsync(loanId);
        if (loan is null) return null;
        if (loan.ReturnDate.HasValue) return loan.ToDto(_lateFeeService);

        loan.ReturnDate = DateTime.Today;
        await _loanRepository.UpdateAsync(loan);
        return loan.ToDto(_lateFeeService);
    }
}
