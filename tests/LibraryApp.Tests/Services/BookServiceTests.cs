using FluentAssertions;
using LibraryApp.Application.Services;
using LibraryApp.Domain.Entities;
using LibraryApp.Domain.Interfaces;
using LibraryApp.Shared.Requests;
using LibraryApp.Tests.Helpers;
using NSubstitute;
using Xunit;

namespace LibraryApp.Tests.Services;

public class BookServiceTests
{
    private readonly IBookRepository _bookRepo = Substitute.For<IBookRepository>();
    private readonly ILoanRepository _loanRepo = Substitute.For<ILoanRepository>();

    private BookService CreateService() => new(_bookRepo, _loanRepo);

    [Fact]
    public async Task GetAllAsync_ReturnsBooksWithAvailability()
    {
        var book = TestDataBuilder.Book(1);
        _bookRepo.GetAllAsync().Returns(new List<Book> { book });
        _loanRepo.IsBookCurrentlyLoanedAsync(1).Returns(true);

        var result = await CreateService().GetAllAsync();

        result.Should().HaveCount(1);
        result[0].IsAvailable.Should().BeFalse();
    }

    [Fact]
    public async Task CreateAsync_PersistsBook()
    {
        var request = new CreateBookRequest
        {
            Title = "X",
            Author = "Y",
            Publisher = "Z",
            PublicationYear = 2020
        };
        Book? saved = null;
        _bookRepo.CreateAsync(Arg.Do<Book>(b => { b.InventoryNumber = 11; saved = b; }))
                 .Returns(c => c.Arg<Book>());

        var result = await CreateService().CreateAsync(request);

        saved.Should().NotBeNull();
        result.InventoryNumber.Should().Be(11);
        result.Title.Should().Be("X");
    }

    [Fact]
    public async Task DeleteAsync_ReturnsFalseWhenNotFound()
    {
        _bookRepo.GetByIdAsync(99).Returns((Book?)null);
        var ok = await CreateService().DeleteAsync(99);
        ok.Should().BeFalse();
    }
}
