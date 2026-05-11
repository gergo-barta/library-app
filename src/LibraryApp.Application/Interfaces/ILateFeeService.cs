namespace LibraryApp.Application.Interfaces;

public interface ILateFeeService
{
    decimal Calculate(DateTime dueDate, DateTime? returnDate);
}
