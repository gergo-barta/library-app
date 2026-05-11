using System.ComponentModel.DataAnnotations;

namespace LibraryApp.Shared.Validation;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
public sealed class NotInPastAttribute : ValidationAttribute
{
    public NotInPastAttribute() : base("Date cannot be in the past.") { }

    public override bool IsValid(object? value)
    {
        if (value is null) return true;
        if (value is DateTime dt) return dt.Date >= DateTime.Today;
        return false;
    }
}
