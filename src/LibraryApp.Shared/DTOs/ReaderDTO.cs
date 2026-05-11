namespace LibraryApp.Shared.DTOs;

public class ReaderDTO
{
    public int ReaderNumber { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
}
