using Microsoft.EntityFrameworkCore;  
using System;  
using System.Collections.Generic;  
using System.Linq;  
using System.Threading.Tasks;  
  
namespace Libraries.Models
{
    public class LibrariesDBContext : DbContext
    {
        public DbSet<Library> Libraries { get; set; }
        public DbSet<Book> Books { get; set; }

        public LibrariesDBContext(DbContextOptions<LibrariesDBContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure Library  
            modelBuilder.Entity<Library>(b => {
                b.HasKey(p => p.Id);
                b.Property(p => p.Name).IsRequired().HasMaxLength(20);
                b.HasMany<Book>(p => p.Books).WithOne(x => x.Library).OnDelete(DeleteBehavior.Cascade);
            });

            // Configure Book  
            modelBuilder.Entity<Book>(b => {
                b.HasKey(p => p.Id);
                b.Property(p => p.Tittle).IsRequired().HasMaxLength(20);
            });

            // Configure Author  
            modelBuilder.Entity<Author>(b => {
                b.HasKey(p => p.Id);
                b.Property(p => p.Name).IsRequired().HasMaxLength(20);
                b.Property(p => p.LastName).IsRequired().HasMaxLength(20);
                b.HasMany<Book>(p => p.Books).WithOne(x => x.Author).OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
