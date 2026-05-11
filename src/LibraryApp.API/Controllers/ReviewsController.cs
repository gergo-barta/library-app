using LibraryApp.API.Auth;
using LibraryApp.Application.Interfaces;
using LibraryApp.Shared.DTOs;
using LibraryApp.Shared.Requests;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApp.API.Controllers;

[ApiController]
[Route("api/reviews")]
public class ReviewsController : ControllerBase
{
    private readonly IReviewService _reviewService;

    public ReviewsController(IReviewService reviewService)
    {
        _reviewService = reviewService;
    }

    [HttpGet("book/{inventoryNumber:int}")]
    public async Task<ActionResult<IReadOnlyList<ReviewDTO>>> GetByBook(int inventoryNumber) =>
        Ok(await _reviewService.GetByBookAsync(inventoryNumber));

    [HttpPost]
    public async Task<ActionResult<ReviewDTO>> Create([FromBody] CreateReviewRequest request)
    {
        var created = await _reviewService.AddAsync(request);
        if (created is null)
        {
            return BadRequest(new { error = "Invalid book or reader." });
        }
        return CreatedAtAction(nameof(GetByBook), new { inventoryNumber = created.InventoryNumber }, created);
    }

    [HttpDelete("{id:int}")]
    [RequireAdminPassword]
    public async Task<IActionResult> Delete(int id)
    {
        var ok = await _reviewService.DeleteAsync(id);
        return ok ? NoContent() : NotFound();
    }
}
