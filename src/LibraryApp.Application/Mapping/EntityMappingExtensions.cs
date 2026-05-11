using LibraryApp.Application.Interfaces;
using LibraryApp.Domain.Entities;
using LibraryApp.Shared.DTOs;
using LibraryApp.Shared.Requests;

namespace LibraryApp.Application.Mapping;

public static class EntityMappingExtensions
{
    public static BookDTO ToDto(this Book book, bool isAvailable = true)
    {
        var reviews = book.Reviews ?? new List<Review>();
        return new BookDTO
        {
            InventoryNumber = book.InventoryNumber,
            Title = book.Title,
            Author = book.Author,
            Publisher = book.Publisher,
            PublicationYear = book.PublicationYear,
            AverageRating = reviews.Count > 0 ? Math.Round(reviews.Average(r => r.Score), 2) : (double?)null,
            ReviewCount = reviews.Count,
            IsAvailable = isAvailable
        };
    }

    public static Book ToEntity(this CreateBookRequest request) => new()
    {
        Title = request.Title,
        Author = request.Author,
        Publisher = request.Publisher,
        PublicationYear = request.PublicationYear
    };

    public static void ApplyTo(this UpdateBookRequest request, Book book)
    {
        book.Title = request.Title;
        book.Author = request.Author;
        book.Publisher = request.Publisher;
        book.PublicationYear = request.PublicationYear;
    }

    public static ReaderDTO ToDto(this Reader reader) => new()
    {
        ReaderNumber = reader.ReaderNumber,
        Name = reader.Name,
        Address = reader.Address,
        DateOfBirth = reader.DateOfBirth
    };

    public static Reader ToEntity(this CreateReaderRequest request) => new()
    {
        Name = request.Name,
        Address = request.Address,
        DateOfBirth = request.DateOfBirth
    };

    public static void ApplyTo(this UpdateReaderRequest request, Reader reader)
    {
        reader.Name = request.Name;
        reader.Address = request.Address;
        reader.DateOfBirth = request.DateOfBirth;
    }

    public static LoanDTO ToDto(this Loan loan, ILateFeeService lateFeeService)
    {
        var lateFee = lateFeeService.Calculate(loan.DueDate, loan.ReturnDate);
        string status;
        if (loan.ReturnDate.HasValue)
        {
            status = loan.ReturnDate.Value.Date > loan.DueDate.Date ? "Returned (late)" : "Returned";
        }
        else
        {
            status = DateTime.Today > loan.DueDate.Date ? "Overdue" : "Active";
        }

        return new LoanDTO
        {
            Id = loan.Id,
            ReaderNumber = loan.ReaderNumber,
            ReaderName = loan.Reader?.Name ?? string.Empty,
            InventoryNumber = loan.InventoryNumber,
            BookTitle = loan.Book?.Title ?? string.Empty,
            LoanDate = loan.LoanDate,
            DueDate = loan.DueDate,
            ReturnDate = loan.ReturnDate,
            LateFee = lateFee,
            Status = status
        };
    }

    public static Loan ToEntity(this CreateLoanRequest request) => new()
    {
        ReaderNumber = request.ReaderNumber,
        InventoryNumber = request.InventoryNumber,
        LoanDate = request.LoanDate,
        DueDate = request.DueDate,
        ReturnDate = null
    };

    public static ReviewDTO ToDto(this Review review) => new()
    {
        Id = review.Id,
        InventoryNumber = review.InventoryNumber,
        BookTitle = review.Book?.Title ?? string.Empty,
        ReaderNumber = review.ReaderNumber,
        ReaderName = review.Reader?.Name ?? string.Empty,
        Score = review.Score,
        Text = review.Text,
        CreatedAt = review.CreatedAt
    };

    public static Review ToEntity(this CreateReviewRequest request) => new()
    {
        InventoryNumber = request.InventoryNumber,
        ReaderNumber = request.ReaderNumber,
        Score = request.Score,
        Text = request.Text,
        CreatedAt = DateTime.UtcNow
    };
}
