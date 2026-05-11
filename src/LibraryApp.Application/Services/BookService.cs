using LibraryApp.Application.Interfaces;
using LibraryApp.Application.Mapping;
using LibraryApp.Domain.Interfaces;
using LibraryApp.Shared.DTOs;
using LibraryApp.Shared.Requests;

namespace LibraryApp.Application.Services;

public class BookService : IBookService
{
    private readonly IBookRepository _bookRepository;
    private readonly ILoanRepository _loanRepository;

    public BookService(IBookRepository bookRepository, ILoanRepository loanRepository)
    {
        _bookRepository = bookRepository;
        _loanRepository = loanRepository;
    }

    public async Task<IReadOnlyList<BookDTO>> GetAllAsync()
    {
        var books = await _bookRepository.GetAllAsync();
        var result = new List<BookDTO>(books.Count);
        foreach (var book in books)
        {
            var isAvailable = !await _loanRepository.IsBookCurrentlyLoanedAsync(book.InventoryNumber);
            result.Add(book.ToDto(isAvailable));
        }
        return result;
    }

    public async Task<BookDTO?> GetByIdAsync(int inventoryNumber)
    {
        var book = await _bookRepository.GetByIdAsync(inventoryNumber);
        if (book is null) return null;
        var isAvailable = !await _loanRepository.IsBookCurrentlyLoanedAsync(inventoryNumber);
        return book.ToDto(isAvailable);
    }

    public async Task<IReadOnlyList<BookDTO>> GetAvailableAsync()
    {
        var books = await _bookRepository.GetAvailableAsync();
        return books.Select(b => b.ToDto(true)).ToList();
    }

    public async Task<BookDTO> CreateAsync(CreateBookRequest request)
    {
        var book = request.ToEntity();
        var saved = await _bookRepository.CreateAsync(book);
        return saved.ToDto(true);
    }

    public async Task<BookDTO?> UpdateAsync(int inventoryNumber, UpdateBookRequest request)
    {
        var existing = await _bookRepository.GetByIdAsync(inventoryNumber);
        if (existing is null) return null;
        request.ApplyTo(existing);
        await _bookRepository.UpdateAsync(existing);
        var isAvailable = !await _loanRepository.IsBookCurrentlyLoanedAsync(inventoryNumber);
        return existing.ToDto(isAvailable);
    }

    public async Task<bool> DeleteAsync(int inventoryNumber)
    {
        var existing = await _bookRepository.GetByIdAsync(inventoryNumber);
        if (existing is null) return false;
        await _bookRepository.DeleteAsync(inventoryNumber);
        return true;
    }
}
