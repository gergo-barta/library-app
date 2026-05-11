using LibraryApp.Application.Configuration;
using LibraryApp.Application.Interfaces;
using Microsoft.Extensions.Options;

namespace LibraryApp.Application.Services;

public class LateFeeService : ILateFeeService
{
    private readonly decimal _baseFee;

    public LateFeeService(IOptions<LateFeeOptions> options)
    {
        _baseFee = options.Value.BaseFee;
    }

    public decimal Calculate(DateTime dueDate, DateTime? returnDate)
    {
        var effectiveDate = returnDate?.Date ?? DateTime.Today;
        var due = dueDate.Date;

        if (effectiveDate <= due)
        {
            return 0m;
        }

        var lateDays = (effectiveDate - due).Days;
        var multiplier = lateDays switch
        {
            <= 10 => 1,
            <= 15 => 2,
            _ => 3
        };

        return _baseFee * lateDays * multiplier;
    }
}
