using FluentAssertions;
using LibraryApp.Application.Configuration;
using LibraryApp.Application.Services;
using Microsoft.Extensions.Options;
using Xunit;

namespace LibraryApp.Tests.Services;

public class LateFeeServiceTests
{
    private const int BaseFee = 50;

    private static LateFeeService CreateService() =>
        new(Options.Create(new LateFeeOptions { BaseFee = BaseFee }));

    [Fact]
    public void Returned_OnTime_ReturnsZero()
    {
        var svc = CreateService();
        var due = DateTime.Today.AddDays(7);
        var returned = DateTime.Today.AddDays(5);

        svc.Calculate(due, returned).Should().Be(0m);
    }

    [Fact]
    public void Returned_OnSameDay_ReturnsZero()
    {
        var svc = CreateService();
        var due = DateTime.Today;

        svc.Calculate(due, due).Should().Be(0m);
    }

    [Fact]
    public void OneDayLate_ReturnsBaseFeeTimesOneTimesOne()
    {
        var svc = CreateService();
        var due = DateTime.Today.AddDays(-1);
        var returned = DateTime.Today;

        svc.Calculate(due, returned).Should().Be(BaseFee * 1 * 1);
    }

    [Fact]
    public void TenDaysLate_ReturnsBaseFeeTimesTenTimesOne()
    {
        var svc = CreateService();
        var due = DateTime.Today.AddDays(-10);
        var returned = DateTime.Today;

        svc.Calculate(due, returned).Should().Be(BaseFee * 10 * 1);
    }

    [Fact]
    public void ElevenDaysLate_AppliesDoubleMultiplier()
    {
        var svc = CreateService();
        var due = DateTime.Today.AddDays(-11);
        var returned = DateTime.Today;

        svc.Calculate(due, returned).Should().Be(BaseFee * 11 * 2);
    }

    [Fact]
    public void FifteenDaysLate_AppliesDoubleMultiplier()
    {
        var svc = CreateService();
        var due = DateTime.Today.AddDays(-15);
        var returned = DateTime.Today;

        svc.Calculate(due, returned).Should().Be(BaseFee * 15 * 2);
    }

    [Fact]
    public void SixteenDaysLate_AppliesTripleMultiplier()
    {
        var svc = CreateService();
        var due = DateTime.Today.AddDays(-16);
        var returned = DateTime.Today;

        svc.Calculate(due, returned).Should().Be(BaseFee * 16 * 3);
    }

    [Fact]
    public void ThirtyDaysLate_AppliesTripleMultiplier()
    {
        var svc = CreateService();
        var due = DateTime.Today.AddDays(-30);
        var returned = DateTime.Today;

        svc.Calculate(due, returned).Should().Be(BaseFee * 30 * 3);
    }

    [Fact]
    public void NotReturned_Overdue_CalculatesAgainstToday()
    {
        var svc = CreateService();
        var due = DateTime.Today.AddDays(-5);

        svc.Calculate(due, null).Should().Be(BaseFee * 5 * 1);
    }

    [Fact]
    public void NotReturned_WithinDueDate_ReturnsZero()
    {
        var svc = CreateService();
        var due = DateTime.Today.AddDays(5);

        svc.Calculate(due, null).Should().Be(0m);
    }
}
