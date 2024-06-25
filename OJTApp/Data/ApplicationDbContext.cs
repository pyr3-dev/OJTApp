using Microsoft.EntityFrameworkCore;
using OJTApp.Models;

namespace OJTApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {

        }

        public DbSet<Person> Persons { get; set; }
    }
}
