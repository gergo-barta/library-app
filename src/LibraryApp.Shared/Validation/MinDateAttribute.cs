using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace LibraryApp.Shared.Validation;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
public sealed class MinDateAttribute : ValidationAttribute
{
    private readonly DateTime _minDate;

    public MinDateAttribute(string minDateIso)
    {
        _minDate = DateTime.Parse(minDateIso, CultureInfo.InvariantCulture);
        ErrorMessage = $"Date must be on or after {_minDate:yyyy-MM-dd}.";
    }

    public override bool IsValid(object? value)
    {
        if (value is null) return true;
        if (value is DateTime dt) return dt.Date >= _minDate.Date;
        return false;
    }
}
