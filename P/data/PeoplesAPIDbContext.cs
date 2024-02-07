using Microsoft.EntityFrameworkCore;
using P.Models;


namespace P.data
{
    public class PeoplesAPIDbContext: DbContext
    {
        public PeoplesAPIDbContext(DbContextOptions options): base(options)
        {
        }

        public DbSet<People> Peoples {  get; set; }
    }
}
