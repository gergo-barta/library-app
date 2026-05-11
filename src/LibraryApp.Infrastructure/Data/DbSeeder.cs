using LibraryApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Infrastructure.Data;

public static class DbSeeder
{
    public static async Task SeedAsync(LibraryDbContext db)
    {
        if (await db.Books.AnyAsync()) return;

        var today = DateTime.Today;

        var books = new List<Book>
        {
            new() { Title = "Egri csillagok", Author = "Gárdonyi Géza", Publisher = "Móra Könyvkiadó", PublicationYear = 1899 },
            new() { Title = "A kőszívű ember fiai", Author = "Jókai Mór", Publisher = "Európa Könyvkiadó", PublicationYear = 1869 },
            new() { Title = "Pál utcai fiúk", Author = "Molnár Ferenc", Publisher = "Móra Könyvkiadó", PublicationYear = 1907 },
            new() { Title = "Abigél", Author = "Szabó Magda", Publisher = "Móra Könyvkiadó", PublicationYear = 1970 },
            new() { Title = "1984", Author = "George Orwell", Publisher = "Secker & Warburg", PublicationYear = 1949 },
            new() { Title = "To Kill a Mockingbird", Author = "Harper Lee", Publisher = "J.B. Lippincott & Co.", PublicationYear = 1960 },
            new() { Title = "The Great Gatsby", Author = "F. Scott Fitzgerald", Publisher = "Charles Scribner's Sons", PublicationYear = 1925 },
            new() { Title = "Brave New World", Author = "Aldous Huxley", Publisher = "Chatto & Windus", PublicationYear = 1932 },
            new() { Title = "Foundation", Author = "Isaac Asimov", Publisher = "Gnome Press", PublicationYear = 1951 },
            new() { Title = "The Hobbit", Author = "J.R.R. Tolkien", Publisher = "George Allen & Unwin", PublicationYear = 1937 },
            new() { Title = "Sapiens", Author = "Yuval Noah Harari", Publisher = "Harvill Secker", PublicationYear = 2011 }
        };
        db.Books.AddRange(books);

        var readers = new List<Reader>
        {
            new() { Name = "Kovács Anna", Address = "1203 Budapest, Ady Endre utca 12.", DateOfBirth = new DateTime(1992, 4, 14) },
            new() { Name = "Nagy Péter", Address = "1052 Budapest, Deák tér 3.", DateOfBirth = new DateTime(1985, 11, 2) },
            new() { Name = "Szabó Júlia", Address = "6720 Szeged, Tisza Lajos krt. 22.", DateOfBirth = new DateTime(2000, 7, 30) },
            new() { Name = "Tóth Bence", Address = "4025 Debrecen, Piac u. 8.", DateOfBirth = new DateTime(1978, 1, 19) },
            new() { Name = "Horváth Eszter", Address = "7621 Pécs, Király u. 5.", DateOfBirth = new DateTime(1995, 9, 23) }
        };
        db.Readers.AddRange(readers);

        await db.SaveChangesAsync();

        var loans = new List<Loan>
        {
            new()
            {
                ReaderNumber = readers[0].ReaderNumber,
                InventoryNumber = books[0].InventoryNumber,
                LoanDate = today.AddDays(-5),
                DueDate = today.AddDays(9),
                ReturnDate = null
            },
            new()
            {
                ReaderNumber = readers[1].ReaderNumber,
                InventoryNumber = books[1].InventoryNumber,
                LoanDate = today.AddDays(-25),
                DueDate = today.AddDays(-11),
                ReturnDate = null
            },
            new()
            {
                ReaderNumber = readers[2].ReaderNumber,
                InventoryNumber = books[2].InventoryNumber,
                LoanDate = today.AddDays(-45),
                DueDate = today.AddDays(-31),
                ReturnDate = null
            },
            new()
            {
                ReaderNumber = readers[3].ReaderNumber,
                InventoryNumber = books[3].InventoryNumber,
                LoanDate = today.AddDays(-30),
                DueDate = today.AddDays(-16),
                ReturnDate = today.AddDays(-17)
            },
            new()
            {
                ReaderNumber = readers[4].ReaderNumber,
                InventoryNumber = books[4].InventoryNumber,
                LoanDate = today.AddDays(-40),
                DueDate = today.AddDays(-26),
                ReturnDate = today.AddDays(-13)
            },
            new()
            {
                ReaderNumber = readers[0].ReaderNumber,
                InventoryNumber = books[5].InventoryNumber,
                LoanDate = today.AddDays(-60),
                DueDate = today.AddDays(-46),
                ReturnDate = today.AddDays(-5)
            }
        };
        db.Loans.AddRange(loans);

        var reviews = new List<Review>
        {
            new() { InventoryNumber = books[0].InventoryNumber, ReaderNumber = readers[0].ReaderNumber, Score = 5, Text = "Időtálló klasszikus, kötelező olvasmány.", CreatedAt = DateTime.UtcNow.AddDays(-30) },
            new() { InventoryNumber = books[0].InventoryNumber, ReaderNumber = readers[2].ReaderNumber, Score = 4, Text = "Nagyon élveztem, csak néha hosszadalmas.", CreatedAt = DateTime.UtcNow.AddDays(-20) },
            new() { InventoryNumber = books[4].InventoryNumber, ReaderNumber = readers[1].ReaderNumber, Score = 5, Text = "A disztópikus regények csúcsa.", CreatedAt = DateTime.UtcNow.AddDays(-15) },
            new() { InventoryNumber = books[4].InventoryNumber, ReaderNumber = readers[3].ReaderNumber, Score = 4, Text = "Ijesztően aktuális ma is.", CreatedAt = DateTime.UtcNow.AddDays(-10) },
            new() { InventoryNumber = books[6].InventoryNumber, ReaderNumber = readers[4].ReaderNumber, Score = 3, Text = "Szép próza, de a karakterek nem nyerték el a tetszésemet.", CreatedAt = DateTime.UtcNow.AddDays(-7) },
            new() { InventoryNumber = books[9].InventoryNumber, ReaderNumber = readers[0].ReaderNumber, Score = 5, Text = "Csodálatos kalandregény minden korosztálynak.", CreatedAt = DateTime.UtcNow.AddDays(-5) },
            new() { InventoryNumber = books[9].InventoryNumber, ReaderNumber = readers[2].ReaderNumber, Score = 5, Text = "Tolkien zsenialitása már itt megmutatkozik.", CreatedAt = DateTime.UtcNow.AddDays(-3) },
            new() { InventoryNumber = books[10].InventoryNumber, ReaderNumber = readers[1].ReaderNumber, Score = 4, Text = "Tanulságos ismeretterjesztő olvasmány.", CreatedAt = DateTime.UtcNow.AddDays(-1) }
        };
        db.Reviews.AddRange(reviews);

        await db.SaveChangesAsync();
    }
}
