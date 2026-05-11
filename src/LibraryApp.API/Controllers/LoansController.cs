using LibraryApp.API.Auth;
using LibraryApp.Application.Interfaces;
using LibraryApp.Shared.DTOs;
using LibraryApp.Shared.Requests;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApp.API.Controllers;

[ApiController]
[Route("api/loans")]
public class LoansController : ControllerBase
{
    private readonly ILoanService _loanService;

    public LoansController(ILoanService loanService)
    {
        _loanService = loanService;
    }

    [HttpGet]
    [RequireAdminPassword]
    public async Task<ActionResult<IReadOnlyList<LoanDTO>>> GetAll() =>
        Ok(await _loanService.GetAllAsync());

    [HttpGet("{id:int}")]
    [RequireAdminPassword]
    public async Task<ActionResult<LoanDTO>> GetById(int id)
    {
        var loan = await _loanService.GetByIdAsync(id);
        return loan is null ? NotFound() : Ok(loan);
    }

    [HttpGet("reader/{readerNumber:int}")]
    public async Task<ActionResult<IReadOnlyList<LoanDTO>>> GetByReader(int readerNumber) =>
        Ok(await _loanService.GetByReaderAsync(readerNumber));

    [HttpPost]
    [RequireAdminPassword]
    public async Task<ActionResult<LoanDTO>> Create([FromBody] CreateLoanRequest request)
    {
        var created = await _loanService.CreateAsync(request);
        if (created is null)
        {
            return BadRequest(new { error = "Invalid book or reader, or book is already loaned out." });
        }
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id:int}/return")]
    [RequireAdminPassword]
    public async Task<ActionResult<LoanDTO>> Return(int id)
    {
        var loan = await _loanService.ReturnBookAsync(id);
        return loan is null ? NotFound() : Ok(loan);
    }

    [HttpDelete("{id:int}")]
    [RequireAdminPassword]
    public async Task<IActionResult> Delete(int id)
    {
        var ok = await _loanService.DeleteAsync(id);
        return ok ? NoContent() : NotFound();
    }
}
