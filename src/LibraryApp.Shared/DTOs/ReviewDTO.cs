namespace LibraryApp.Shared.DTOs;

public class ReviewDTO
{
    public int Id { get; set; }
    public int InventoryNumber { get; set; }
    public string BookTitle { get; set; } = string.Empty;
    public int ReaderNumber { get; set; }
    public string ReaderName { get; set; } = string.Empty;
    public int Score { get; set; }
    public string Text { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}
