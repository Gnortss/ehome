using web.Models;
using Microsoft.EntityFrameworkCore;

namespace web.Data
{
    public class EhomeContext : DbContext
    {
        public EhomeContext(DbContextOptions<EhomeContext> options) : base(options)
        {

        }

        public DbSet<Listing> Listings { get; set; }
        public DbSet<ListingType> ListingType { get; set; }
        public DbSet<RealEstateType> RealEstateType { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<ListingType>().Property(e => e.Type).ValueGeneratedNever();
            modelBuilder.Entity<RealEstateType>().Property(e => e.Type).ValueGeneratedNever();

            modelBuilder.Entity<Listing>().ToTable("Listing");
        }
    }
}