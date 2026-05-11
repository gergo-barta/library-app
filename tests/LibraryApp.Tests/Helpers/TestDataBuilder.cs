using LibraryApp.Domain.Entities;

namespace LibraryApp.Tests.Helpers;

public static class TestDataBuilder
{
    public static Book Book(int id = 1, string title = "Test Book", string author = "Test Author") => new()
    {
        InventoryNumber = id,
        Title = title,
        Author = author,
        Publisher = "Test Publisher",
        PublicationYear = 2020
    };

    public static Reader Reader(int number = 1, string name = "Test Reader") => new()
    {
        ReaderNumber = number,
        Name = name,
        Address = "Test Address",
        DateOfBirth = new DateTime(1990, 1, 1)
    };

    public static Loan Loan(int id = 1, int readerNumber = 1, int inventoryNumber = 1,
        DateTime? loanDate = null, DateTime? dueDate = null, DateTime? returnDate = null)
    {
        var loan = new Loan
        {
            Id = id,
            ReaderNumber = readerNumber,
            InventoryNumber = inventoryNumber,
            LoanDate = loanDate ?? DateTime.Today.AddDays(-7),
            DueDate = dueDate ?? DateTime.Today.AddDays(7),
            ReturnDate = returnDate
        };
        return loan;
    }

    public static Review Review(int id = 1, int inventoryNumber = 1, int readerNumber = 1, int score = 5)
        => new()
        {
            Id = id,
            InventoryNumber = inventoryNumber,
            ReaderNumber = readerNumber,
            Score = score,
            Text = "Great book",
            CreatedAt = DateTime.UtcNow.AddDays(-1)
        };
}
