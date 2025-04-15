using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ScentifyWebApp.Models.Entities;

namespace ScentifyWebApp.DAL.DB
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Perfume> Perfume { get; set; }
        public DbSet<Invoice> Invoice { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);

        //    // Configuring the PriceInfo property to store as JSON in the database
        //    modelBuilder.Entity<Perfume>()
        //        .Property(p => p.PriceInfo)
        //        .HasConversion(
        //            v => JsonSerializer.Serialize(v, null), // Convert to JSON when saving to database
        //            v => JsonSerializer.Deserialize<List<PriceInfo>>(v, null) // Deserialize JSON when reading from the database
        //        );
        //}
    }
}
