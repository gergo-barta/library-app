using System.ComponentModel.DataAnnotations;
using LibraryApp.Shared.Validation;

namespace LibraryApp.Shared.Requests;

public class CreateReviewRequest
{
    public int InventoryNumber { get; set; }
    public int ReaderNumber { get; set; }

    [Range(1, 5)]
    public int Score { get; set; }

    [Required]
    [NotWhiteSpace]
    public string Text { get; set; } = string.Empty;
}
