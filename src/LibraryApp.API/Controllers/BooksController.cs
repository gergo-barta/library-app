using LibraryApp.API.Auth;
using LibraryApp.Application.Interfaces;
using LibraryApp.Shared.DTOs;
using LibraryApp.Shared.Requests;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApp.API.Controllers;

[ApiController]
[Route("api/books")]
public class BooksController : ControllerBase
{
    private readonly IBookService _bookService;

    public BooksController(IBookService bookService)
    {
        _bookService = bookService;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<BookDTO>>> GetAll() =>
        Ok(await _bookService.GetAllAsync());

    [HttpGet("available")]
    public async Task<ActionResult<IReadOnlyList<BookDTO>>> GetAvailable() =>
        Ok(await _bookService.GetAvailableAsync());

    [HttpGet("{id:int}")]
    public async Task<ActionResult<BookDTO>> GetById(int id)
    {
        var book = await _bookService.GetByIdAsync(id);
        return book is null ? NotFound() : Ok(book);
    }

    [HttpPost]
    [RequireAdminPassword]
    public async Task<ActionResult<BookDTO>> Create([FromBody] CreateBookRequest request)
    {
        var created = await _bookService.CreateAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = created.InventoryNumber }, created);
    }

    [HttpPut("{id:int}")]
    [RequireAdminPassword]
    public async Task<ActionResult<BookDTO>> Update(int id, [FromBody] UpdateBookRequest request)
    {
        var updated = await _bookService.UpdateAsync(id, request);
        return updated is null ? NotFound() : Ok(updated);
    }

    [HttpDelete("{id:int}")]
    [RequireAdminPassword]
    public async Task<IActionResult> Delete(int id)
    {
        var ok = await _bookService.DeleteAsync(id);
        return ok ? NoContent() : NotFound();
    }
}
