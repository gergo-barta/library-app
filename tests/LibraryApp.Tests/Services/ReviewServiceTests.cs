using FluentAssertions;
using LibraryApp.Application.Services;
using LibraryApp.Domain.Entities;
using LibraryApp.Domain.Interfaces;
using LibraryApp.Shared.Requests;
using LibraryApp.Tests.Helpers;
using NSubstitute;
using Xunit;

namespace LibraryApp.Tests.Services;

public class ReviewServiceTests
{
    private readonly IReviewRepository _reviewRepo = Substitute.For<IReviewRepository>();
    private readonly IBookRepository _bookRepo = Substitute.For<IBookRepository>();
    private readonly IReaderRepository _readerRepo = Substitute.For<IReaderRepository>();

    private ReviewService CreateService() => new(_reviewRepo, _bookRepo, _readerRepo);

    [Fact]
    public async Task AddAsync_ReturnsNullWhenBookMissing()
    {
        _bookRepo.GetByIdAsync(1).Returns((Book?)null);
        var result = await CreateService().AddAsync(new CreateReviewRequest
        {
            InventoryNumber = 1,
            ReaderNumber = 1,
            Score = 5,
            Text = "ok"
        });
        result.Should().BeNull();
    }

    [Fact]
    public async Task AddAsync_PersistsReview()
    {
        var book = TestDataBuilder.Book(1);
        var reader = TestDataBuilder.Reader(1);
        _bookRepo.GetByIdAsync(1).Returns(book);
        _readerRepo.GetByIdAsync(1).Returns(reader);

        Review? saved = null;
        _reviewRepo.CreateAsync(Arg.Do<Review>(r => { r.Id = 7; saved = r; }))
                   .Returns(c => c.Arg<Review>());

        var stored = TestDataBuilder.Review(7, 1, 1, 4);
        stored.Book = book;
        stored.Reader = reader;
        _reviewRepo.GetByIdAsync(7).Returns(stored);

        var result = await CreateService().AddAsync(new CreateReviewRequest
        {
            InventoryNumber = 1,
            ReaderNumber = 1,
            Score = 4,
            Text = "ok"
        });

        saved.Should().NotBeNull();
        result.Should().NotBeNull();
        result!.Id.Should().Be(7);
        result.Score.Should().Be(4);
    }

    [Fact]
    public async Task GetByBookAsync_ReturnsReviews()
    {
        var reader = TestDataBuilder.Reader(1);
        var review = TestDataBuilder.Review(1, 5, 1);
        review.Reader = reader;
        _reviewRepo.GetByBookAsync(5).Returns(new List<Review> { review });

        var result = await CreateService().GetByBookAsync(5);
        result.Should().HaveCount(1);
    }

    [Fact]
    public async Task DeleteAsync_ReturnsTrueWhenFound()
    {
        var review = TestDataBuilder.Review(3);
        _reviewRepo.GetByIdAsync(3).Returns(review);

        var ok = await CreateService().DeleteAsync(3);

        ok.Should().BeTrue();
        await _reviewRepo.Received(1).DeleteAsync(3);
    }
}
