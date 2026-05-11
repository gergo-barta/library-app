namespace LibraryApp.Domain.Entities;

public class Reader
{
    public int ReaderNumber { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }

    public ICollection<Loan> Loans { get; set; } = new List<Loan>();
    public ICollection<Review> Reviews { get; set; } = new List<Review>();
}
