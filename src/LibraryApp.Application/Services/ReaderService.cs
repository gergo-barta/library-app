using LibraryApp.Application.Interfaces;
using LibraryApp.Application.Mapping;
using LibraryApp.Domain.Interfaces;
using LibraryApp.Shared.DTOs;
using LibraryApp.Shared.Requests;

namespace LibraryApp.Application.Services;

public class ReaderService : IReaderService
{
    private readonly IReaderRepository _readerRepository;

    public ReaderService(IReaderRepository readerRepository)
    {
        _readerRepository = readerRepository;
    }

    public async Task<IReadOnlyList<ReaderDTO>> GetAllAsync()
    {
        var readers = await _readerRepository.GetAllAsync();
        return readers.Select(r => r.ToDto()).ToList();
    }

    public async Task<ReaderDTO?> GetByReaderNumberAsync(int readerNumber)
    {
        var reader = await _readerRepository.GetByIdAsync(readerNumber);
        return reader?.ToDto();
    }

    public async Task<ReaderDTO> CreateAsync(CreateReaderRequest request)
    {
        var reader = request.ToEntity();
        var saved = await _readerRepository.CreateAsync(reader);
        return saved.ToDto();
    }

    public async Task<ReaderDTO?> UpdateAsync(int readerNumber, UpdateReaderRequest request)
    {
        var existing = await _readerRepository.GetByIdAsync(readerNumber);
        if (existing is null) return null;
        request.ApplyTo(existing);
        await _readerRepository.UpdateAsync(existing);
        return existing.ToDto();
    }

    public async Task<bool> DeleteAsync(int readerNumber)
    {
        var existing = await _readerRepository.GetByIdAsync(readerNumber);
        if (existing is null) return false;
        await _readerRepository.DeleteAsync(readerNumber);
        return true;
    }
}
