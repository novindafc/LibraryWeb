using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LibraryWeb.Models;

namespace LibraryWeb.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<BookLog> BookLog { get; set; }
        public DbSet<Book> Book { get; set; }
        public DbSet<Member> Member { get; set; }
        
        public AppDbContext  (DbContextOptions<AppDbContext > options)
            : base(options)
        {
        }

       
    }
}
