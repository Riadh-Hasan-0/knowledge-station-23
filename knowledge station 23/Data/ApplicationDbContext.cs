using Microsoft.EntityFrameworkCore;

namespace knowledge_station_23.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions options) 
            : base(options)
        {

        }
    }
}
