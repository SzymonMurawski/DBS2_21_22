using DVDRentalStoreWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace DVDRentalStoreWebApp.DAL
{
    public class StoreContext : DbContext
    {
        public StoreContext(DbContextOptions<StoreContext> options) : base(options)
        { }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Copy> Copies { get; set; }
    }
}
