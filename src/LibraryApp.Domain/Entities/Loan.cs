namespace LibraryApp.Domain.Entities;

public class Loan
{
    public int Id { get; set; }
    public int ReaderNumber { get; set; }
    public int InventoryNumber { get; set; }
    public DateTime LoanDate { get; set; }
    public DateTime DueDate { get; set; }
    public DateTime? ReturnDate { get; set; }

    public Reader Reader { get; set; } = null!;
    public Book Book { get; set; } = null!;
}
