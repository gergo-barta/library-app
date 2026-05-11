using System.ComponentModel.DataAnnotations;
using LibraryApp.Shared.Validation;

namespace LibraryApp.Shared.Requests;

public class LoginRequest
{
    [Required]
    [NotWhiteSpace]
    public string Password { get; set; } = string.Empty;
}
