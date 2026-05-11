namespace LibraryApp.Domain.Entities;

public class Review
{
    public int Id { get; set; }
    public int InventoryNumber { get; set; }
    public int ReaderNumber { get; set; }
    public int Score { get; set; }
    public string Text { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }

    public Book Book { get; set; } = null!;
    public Reader Reader { get; set; } = null!;
}
