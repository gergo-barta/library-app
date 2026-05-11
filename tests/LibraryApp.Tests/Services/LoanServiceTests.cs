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
