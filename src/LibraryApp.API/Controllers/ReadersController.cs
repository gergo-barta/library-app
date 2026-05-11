using LibraryApp.API.Auth;
using LibraryApp.Application.Interfaces;
using LibraryApp.Shared.DTOs;
using LibraryApp.Shared.Requests;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApp.API.Controllers;

[ApiController]
[Route("api/readers")]
public class ReadersController : ControllerBase
{
    private readonly IReaderService _readerService;

    public ReadersController(IReaderService readerService)
    {
        _readerService = readerService;
    }

    [HttpGet]
    [RequireAdminPassword]
    public async Task<ActionResult<IReadOnlyList<ReaderDTO>>> GetAll() =>
        Ok(await _readerService.GetAllAsync());

    [HttpGet("{readerNumber:int}")]
    public async Task<ActionResult<ReaderDTO>> GetByReaderNumber(int readerNumber)
    {
        var reader = await _readerService.GetByReaderNumberAsync(readerNumber);
        return reader is null ? NotFound() : Ok(reader);
    }

    [HttpPost]
    [RequireAdminPassword]
    public async Task<ActionResult<ReaderDTO>> Create([FromBody] CreateReaderRequest request)
    {
        var created = await _readerService.CreateAsync(request);
        return CreatedAtAction(nameof(GetByReaderNumber), new { readerNumber = created.ReaderNumber }, created);
    }

    [HttpPut("{readerNumber:int}")]
    [RequireAdminPassword]
    public async Task<ActionResult<ReaderDTO>> Update(int readerNumber, [FromBody] UpdateReaderRequest request)
    {
        var updated = await _readerService.UpdateAsync(readerNumber, request);
        return updated is null ? NotFound() : Ok(updated);
    }

    [HttpDelete("{readerNumber:int}")]
    [RequireAdminPassword]
    public async Task<IActionResult> Delete(int readerNumber)
    {
        var ok = await _readerService.DeleteAsync(readerNumber);
        return ok ? NoContent() : NotFound();
    }
}
