using Microsoft.EntityFrameworkCore;  
using System;  
using System.Collections.Generic;  
using System.Linq;  
using System.Threading.Tasks;  
  
namespace Libraries.Models
{
    public class LibrariesDBContext : DbContext
    {  
        public LibrariesDBContext(DbContextOptions<LibrariesDBContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
             }
    }
}
