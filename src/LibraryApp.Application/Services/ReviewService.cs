using LibraryApp.Application.Interfaces;
using LibraryApp.Application.Mapping;
using LibraryApp.Domain.Interfaces;
using LibraryApp.Shared.DTOs;
using LibraryApp.Shared.Requests;

namespace LibraryApp.Application.Services;

public class ReviewService : IReviewService
{
    private readonly IReviewRepository _reviewRepository;
    private readonly IBookRepository _bookRepository;
    private readonly IReaderRepository _readerRepository;

    public ReviewService(
        IReviewRepository reviewRepository,
        IBookRepository bookRepository,
        IReaderRepository readerRepository)
    {
        _reviewRepository = reviewRepository;
        _bookRepository = bookRepository;
        _readerRepository = readerRepository;
    }

    public async Task<IReadOnlyList<ReviewDTO>> GetByBookAsync(int inventoryNumber)
    {
        var reviews = await _reviewRepository.GetByBookAsync(inventoryNumber);
        return reviews.Select(r => r.ToDto()).ToList();
    }

    public async Task<ReviewDTO?> AddAsync(CreateReviewRequest request)
    {
        var book = await _bookRepository.GetByIdAsync(request.InventoryNumber);
        if (book is null) return null;
        var reader = await _readerRepository.GetByIdAsync(request.ReaderNumber);
        if (reader is null) return null;

        var review = request.ToEntity();
        var saved = await _reviewRepository.CreateAsync(review);
        var fresh = await _reviewRepository.GetByIdAsync(saved.Id);
        return fresh!.ToDto();
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var existing = await _reviewRepository.GetByIdAsync(id);
        if (existing is null) return false;
        await _reviewRepository.DeleteAsync(id);
        return true;
    }
}
