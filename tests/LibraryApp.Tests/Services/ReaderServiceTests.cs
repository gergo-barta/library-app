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
    public async Task CreateAsync_PersistsReader()
    {
        Reader? saved = null;
        _readerRepo.CreateAsync(Arg.Do<Reader>(r => { r.ReaderNumber = 9; saved = r; }))
                   .Returns(c => c.Arg<Reader>());

        var result = await CreateService().CreateAsync(new CreateReaderRequest
        {
            Name = "Bob",
            Address = "1051 Bp",
            DateOfBirth = new DateTime(1990, 1, 1)
        });

        saved.Should().NotBeNull();
        result.ReaderNumber.Should().Be(9);
        result.Name.Should().Be("Bob");
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
