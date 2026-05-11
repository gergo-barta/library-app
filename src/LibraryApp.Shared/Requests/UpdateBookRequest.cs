using System.ComponentModel.DataAnnotations;
using LibraryApp.Shared.Validation;

namespace LibraryApp.Shared.Requests;

public class UpdateBookRequest
{
    [Required]
    [NotWhiteSpace]
    public string Title { get; set; } = string.Empty;

    [Required]
    [NotWhiteSpace]
    public string Author { get; set; } = string.Empty;

    [Required]
    [NotWhiteSpace]
    public string Publisher { get; set; } = string.Empty;

    [Range(0, int.MaxValue)]
    public int PublicationYear { get; set; }
}
