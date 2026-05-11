namespace LibraryApp.Shared.DTOs;

public class BookDTO
{
    public int InventoryNumber { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public string Publisher { get; set; } = string.Empty;
    public int PublicationYear { get; set; }
    public double? AverageRating { get; set; }
    public int ReviewCount { get; set; }
    public bool IsAvailable { get; set; }
}
