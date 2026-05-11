using System.ComponentModel.DataAnnotations;

namespace LibraryApp.Shared.Validation;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
public sealed class NotWhiteSpaceAttribute : ValidationAttribute
{
    public NotWhiteSpaceAttribute() : base("The field must not be empty or whitespace.") { }

    public override bool IsValid(object? value)
    {
        if (value is null) return false;
        if (value is string s) return !string.IsNullOrWhiteSpace(s);
        return true;
    }
}
