using FluentAssertions;
using LibraryApp.Application.Interfaces;
using LibraryApp.Application.Services;
using LibraryApp.Domain.Entities;
using LibraryApp.Domain.Interfaces;
using LibraryApp.Shared.Requests;
using LibraryApp.Tests.Helpers;
using NSubstitute;
using Xunit;

namespace LibraryApp.Tests.Services;

public class LoanServiceTests
{
    private readonly ILoanRepository _loanRepo = Substitute.For<ILoanRepository>();
    private readonly IBookRepository _bookRepo = Substitute.For<IBookRepository>();
    private readonly IReaderRepository _readerRepo = Substitute.For<IReaderRepository>();
    private readonly ILateFeeService _lateFee = Substitute.For<ILateFeeService>();

    private LoanService CreateService() => new(_loanRepo, _bookRepo, _readerRepo, _lateFee);

    [Fact]
    public async Task GetByReaderAsync_ReturnsOnlyThatReadersLoans()
    {
        var reader = TestDataBuilder.Reader(1);
        var book = TestDataBuilder.Book(1);
        var loan = TestDataBuilder.Loan(1, reader.ReaderNumber, book.InventoryNumber);
        loan.Reader = reader;
        loan.Book = book;
        _loanRepo.GetByReaderAsync(1).Returns(new List<Loan> { loan });
        _lateFee.Calculate(loan.DueDate, loan.ReturnDate).Returns(0m);

        var service = CreateService();
        var result = await service.GetByReaderAsync(1);

        result.Should().HaveCount(1);
        result[0].ReaderNumber.Should().Be(1);
        await _loanRepo.Received(1).GetByReaderAsync(1);
    }

    [Fact]
    public async Task CreateLoanAsync_PersistsLoan()
    {
        var reader = TestDataBuilder.Reader(1);
        var book = TestDataBuilder.Book(1);
        _readerRepo.GetByIdAsync(1).Returns(reader);
        _bookRepo.GetByIdAsync(1).Returns(book);
        _loanRepo.IsBookCurrentlyLoanedAsync(1).Returns(false);

        Loan? saved = null;
        _loanRepo.CreateAsync(Arg.Do<Loan>(l => { l.Id = 42; saved = l; }))
                 .Returns(c => c.Arg<Loan>());

        var savedWithRefs = TestDataBuilder.Loan(42, 1, 1);
        savedWithRefs.Reader = reader;
        savedWithRefs.Book = book;
        _loanRepo.GetByIdAsync(42).Returns(savedWithRefs);
        _lateFee.Calculate(Arg.Any<DateTime>(), Arg.Any<DateTime?>()).Returns(0m);

        var request = new CreateLoanRequest
        {
            ReaderNumber = 1,
            InventoryNumber = 1,
            LoanDate = DateTime.Today,
            DueDate = DateTime.Today.AddDays(14)
        };

        var result = await CreateService().CreateAsync(request);

        result.Should().NotBeNull();
        result!.Id.Should().Be(42);
        saved.Should().NotBeNull();
        saved!.ReaderNumber.Should().Be(1);
        saved.InventoryNumber.Should().Be(1);
    }

    [Fact]
    public async Task CreateLoanAsync_ReturnsNullIfBookAlreadyLoaned()
    {
        _readerRepo.GetByIdAsync(1).Returns(TestDataBuilder.Reader(1));
        _bookRepo.GetByIdAsync(1).Returns(TestDataBuilder.Book(1));
        _loanRepo.IsBookCurrentlyLoanedAsync(1).Returns(true);

        var request = new CreateLoanRequest
        {
            ReaderNumber = 1,
            InventoryNumber = 1,
            LoanDate = DateTime.Today,
            DueDate = DateTime.Today.AddDays(14)
        };

        var result = await CreateService().CreateAsync(request);
        result.Should().BeNull();
    }

    [Fact]
    public async Task ReturnBookAsync_SetsReturnDate()
    {
        var reader = TestDataBuilder.Reader(1);
        var book = TestDataBuilder.Book(1);
        var loan = TestDataBuilder.Loan(5, 1, 1, DateTime.Today.AddDays(-3), DateTime.Today.AddDays(11));
        loan.Reader = reader;
        loan.Book = book;
        _loanRepo.GetByIdAsync(5).Returns(loan);
        _lateFee.Calculate(loan.DueDate, Arg.Any<DateTime?>()).Returns(0m);

        var result = await CreateService().ReturnBookAsync(5);

        result.Should().NotBeNull();
        loan.ReturnDate.Should().Be(DateTime.Today);
        await _loanRepo.Received(1).UpdateAsync(loan);
    }

    [Fact]
    public async Task GetAllAsync_PopulatesLateFeeFromLateFeeService()
    {
        var reader = TestDataBuilder.Reader(1);
        var book = TestDataBuilder.Book(1);
        var loan = TestDataBuilder.Loan(1, 1, 1, DateTime.Today.AddDays(-20), DateTime.Today.AddDays(-6));
        loan.Reader = reader;
        loan.Book = book;
        _loanRepo.GetAllAsync().Returns(new List<Loan> { loan });
        _lateFee.Calculate(loan.DueDate, loan.ReturnDate).Returns(123.45m);

        var result = await CreateService().GetAllAsync();

        result.Should().HaveCount(1);
        result[0].LateFee.Should().Be(123.45m);
        _lateFee.Received(1).Calculate(loan.DueDate, loan.ReturnDate);
    }
}
