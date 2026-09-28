using Microsoft.EntityFrameworkCore;
using Noted.Models;

namespace Noted.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Notes> Notes { get; set; }
    }
}
