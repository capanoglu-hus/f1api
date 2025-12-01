using f1api.Models;
using Microsoft.EntityFrameworkCore;


namespace f1api.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) :DbContext(options)
    {
        public DbSet<Driver> Drivers => Set<Driver>();
    }
}
