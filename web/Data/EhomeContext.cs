using web.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Linq;

namespace web.Data
{
    public class EhomeContext : IdentityDbContext<ApplicationUser>
    {
        public EhomeContext(DbContextOptions<EhomeContext> options) : base(options)
        {

        }

        public DbSet<Listing> Listings { get; set; }
        public DbSet<ListingType> ListingType { get; set; }
        public DbSet<RealEstateGroup> RealEstateGroup { get; set; }
        public DbSet<RealEstateType> RealEstateType { get; set; }
        public DbSet<Favorite> Favorite { get; set; }
        public DbSet<Region> Region { get; set; }

        public DbSet<SizeOption> SizeOptions { get; set; }
        public DbSet<YearOption> YearOptions { get; set; }
        public DbSet<PriceOption> PriceOptions { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ListingType>().Property(e => e.Id).ValueGeneratedNever();
            modelBuilder.Entity<RealEstateType>().Property(e => e.Id).ValueGeneratedNever();
            modelBuilder.Entity<RealEstateGroup>().Property(e => e.Id).ValueGeneratedNever();
            modelBuilder.Entity<Region>().Property(e => e.Id).ValueGeneratedNever();
            modelBuilder.Entity<SizeOption>().Property(e => e.Id).ValueGeneratedNever();
            modelBuilder.Entity<YearOption>().Property(e => e.Id).ValueGeneratedNever();
            modelBuilder.Entity<PriceOption>().Property(e => e.Id).ValueGeneratedNever();

            modelBuilder.Entity<Listing>().ToTable("Listing");
            modelBuilder.Entity<SizeOption>().ToTable("SizeOption");
            modelBuilder.Entity<YearOption>().ToTable("YearOption");
            modelBuilder.Entity<PriceOption>().ToTable("PriceOption");
        }

        public void DetachLocal(Listing t, int entryId)
        {
            var local = this.Set<Listing>()
                .Local
                .FirstOrDefault(entry => entry.Id == entryId);
            if (!(local == null))
            {
                this.Entry(local).State = EntityState.Detached;
            }
            this.Entry(t).State = EntityState.Modified;
        }
    }
}