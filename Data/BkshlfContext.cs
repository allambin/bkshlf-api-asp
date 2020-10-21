using BKSHLF.Models;
using Microsoft.EntityFrameworkCore;

namespace BKSHLF.Data
{
    public class BkshlfContext : DbContext
    {
        public BkshlfContext(DbContextOptions<BkshlfContext> opt) : base(opt)
        {
            
        }

        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Edition> Editions { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<BookAuthor> BookAuthors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookAuthor>()
                .HasKey(ba => new { ba.BookId, ba.AuthorId });
            modelBuilder.Entity<BookAuthor>()
                .HasOne(ba => ba.Book)
                .WithMany(b => b.BooksAuthors)
                .HasForeignKey(ba => ba.BookId);
            modelBuilder.Entity<BookAuthor>()
                .HasOne(ba => ba.Author)
                .WithMany(a => a.BooksAuthors)
                .HasForeignKey(bc => bc.AuthorId);
            modelBuilder.Entity<BookAuthor>()
                .ToTable("BooksAuthors")
                .Property(a => a.Type)
                .HasConversion<string>();

                
            modelBuilder.Entity<Publisher>()
                .HasIndex(a => a.Name)
                .IsUnique();
            modelBuilder.Entity<Edition>()
                .HasIndex(a => a.ISBN)
                .IsUnique();
            modelBuilder.Entity<Edition>()
                .HasIndex(a => a.ISBN13)
                .IsUnique();
        }
    }
}