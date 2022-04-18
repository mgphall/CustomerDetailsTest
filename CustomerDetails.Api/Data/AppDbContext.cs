using CustomerDetails.Core;
using Microsoft.EntityFrameworkCore;

namespace CustomerDetails.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
        {

        }
       


        public DbSet<Profession> Profession { get; set; }

        public DbSet<Person> Person { get; set; }

    }
}
