using GraphQLPlayground.Data;
using Microsoft.EntityFrameworkCore;

namespace GraphQLPlayground
{
    public class BookContext : DbContext
    {
        public BookContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Book> Books { get; set; } = default!;

        public DbSet<Author> Authors { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Author>()
                .HasMany(t => t.Books)
                .WithOne(t => t.Author!)
                .HasForeignKey(t => t.AuthorId);

            modelBuilder
                .Entity<Book>()
                .HasOne(t => t.Author)
                .WithMany(t => t!.Books)
                .HasForeignKey(t => t.AuthorId);

            modelBuilder
                .Entity<Author>()
                .HasData(new[]
                {
                    new Author 
                    { 
                        Id = 1, 
                        Name = "A"
                    }
                });

            modelBuilder
                .Entity<Book>()
                .HasData(new[]
                {
                    new Book
                    {
                        Id = 1,
                        AuthorId = 1,
                        Title = "A"
                    },
                    new Book
                    {
                        Id = 2,
                        AuthorId = 1,
                        Title = "B"
                    },
                });

        }
    }
}
