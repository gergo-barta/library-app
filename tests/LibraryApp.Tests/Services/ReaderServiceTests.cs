using FluentAssertions;
using LibraryApp.Application.Services;
using LibraryApp.Domain.Entities;
using LibraryApp.Domain.Interfaces;
using LibraryApp.Shared.Requests;
using LibraryApp.Tests.Helpers;
using NSubstitute;
using Xunit;

namespace LibraryApp.Tests.Services;

public class ReaderServiceTests
{
    private readonly IReaderRepository _readerRepo = Substitute.For<IReaderRepository>();

    private ReaderService CreateService() => new(_readerRepo);

    [Fact]
    public async Task GetByReaderNumberAsync_ReturnsReader()
    {
        var reader = TestDataBuilder.Reader(7, "Alice");
        _readerRepo.GetByIdAsync(7).Returns(reader);

        var result = await CreateService().GetByReaderNumberAsync(7);

        result.Should().NotBeNull();
        result!.Name.Should().Be("Alice");
    }

    [Fact]
    public async Task GetByReaderNumberAsync_ReturnsNullWhenNotFound()
    {
        _readerRepo.GetByIdAsync(123).Returns((Reader?)null);
        var result = await CreateService().GetByReaderNumberAsync(123);
        result.Should().BeNull();
    }

    [Fact]
    public async Task UpdateAsync_ReturnsNullWhenNotFound()
    {
        _readerRepo.GetByIdAsync(9).Returns((Reader?)null);
        var result = await CreateService().UpdateAsync(9, new UpdateReaderRequest
        {
            Name = "X",
            Address = "Y",
            DateOfBirth = new DateTime(1990, 1, 1)
        });
        result.Should().BeNull();
    }
}
