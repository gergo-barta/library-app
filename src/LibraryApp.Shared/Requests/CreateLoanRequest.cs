using System.ComponentModel.DataAnnotations;
using LibraryApp.Shared.Validation;

namespace LibraryApp.Shared.Requests;

public class CreateLoanRequest : IValidatableObject
{
    public int ReaderNumber { get; set; }
    public int InventoryNumber { get; set; }

    [NotInPast]
    public DateTime LoanDate { get; set; }

    public DateTime DueDate { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (DueDate <= LoanDate)
        {
            yield return new ValidationResult(
                "DueDate must be after LoanDate.",
                new[] { nameof(DueDate) });
        }
    }
}
