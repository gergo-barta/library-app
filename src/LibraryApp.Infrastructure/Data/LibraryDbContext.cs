using LibraryApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Infrastructure.Data;

public class LibraryDbContext : DbContext
{
    public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options) { }

    public DbSet<Book> Books => Set<Book>();
    public DbSet<Reader> Readers => Set<Reader>();
    public DbSet<Loan> Loans => Set<Loan>();
    public DbSet<Review> Reviews => Set<Review>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Book>(b =>
        {
            b.HasKey(x => x.InventoryNumber);
            b.Property(x => x.InventoryNumber).ValueGeneratedOnAdd();
            b.Property(x => x.Title).IsRequired().HasMaxLength(300);
            b.Property(x => x.Author).IsRequired().HasMaxLength(200);
            b.Property(x => x.Publisher).IsRequired().HasMaxLength(200);
        });

        modelBuilder.Entity<Reader>(r =>
        {
            r.HasKey(x => x.ReaderNumber);
            r.Property(x => x.ReaderNumber).ValueGeneratedOnAdd();
            r.Property(x => x.Name).IsRequired().HasMaxLength(200);
            r.Property(x => x.Address).IsRequired().HasMaxLength(300);
        });

        modelBuilder.Entity<Loan>(l =>
        {
            l.HasKey(x => x.Id);
            l.HasOne(x => x.Reader)
                .WithMany(r => r.Loans)
                .HasForeignKey(x => x.ReaderNumber)
                .OnDelete(DeleteBehavior.Restrict);

            l.HasOne(x => x.Book)
                .WithMany(b => b.Loans)
                .HasForeignKey(x => x.InventoryNumber)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Review>(rv =>
        {
            rv.HasKey(x => x.Id);
            rv.Property(x => x.Text).IsRequired().HasMaxLength(2000);

            rv.HasOne(x => x.Book)
                .WithMany(b => b.Reviews)
                .HasForeignKey(x => x.InventoryNumber)
                .OnDelete(DeleteBehavior.Cascade);

            rv.HasOne(x => x.Reader)
                .WithMany(r => r.Reviews)
                .HasForeignKey(x => x.ReaderNumber)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}
