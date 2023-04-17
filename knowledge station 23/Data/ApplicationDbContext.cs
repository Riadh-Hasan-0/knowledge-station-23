using knowledge_station_23.Models;
using Microsoft.EntityFrameworkCore;

namespace knowledge_station_23.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions options) 
            : base(options)
        {

        }
        public DbSet<Book> BookList { get; set; }
        public DbSet<Author> AuthorList { get; set; }
    }
}
