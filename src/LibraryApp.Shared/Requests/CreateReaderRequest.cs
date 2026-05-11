using System.ComponentModel.DataAnnotations;
using LibraryApp.Shared.Validation;

namespace LibraryApp.Shared.Requests;

public class CreateReaderRequest
{
    [Required]
    [NotWhiteSpace]
    public string Name { get; set; } = string.Empty;

    [Required]
    [NotWhiteSpace]
    public string Address { get; set; } = string.Empty;

    [MinDate("1900-01-01")]
    public DateTime DateOfBirth { get; set; }
}
