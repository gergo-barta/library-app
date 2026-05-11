namespace LibraryApp.Shared.DTOs;

public class LoanDTO
{
    public int Id { get; set; }
    public int ReaderNumber { get; set; }
    public string ReaderName { get; set; } = string.Empty;
    public int InventoryNumber { get; set; }
    public string BookTitle { get; set; } = string.Empty;
    public DateTime LoanDate { get; set; }
    public DateTime DueDate { get; set; }
    public DateTime? ReturnDate { get; set; }
    public decimal LateFee { get; set; }
    public string Status { get; set; } = string.Empty;
}
