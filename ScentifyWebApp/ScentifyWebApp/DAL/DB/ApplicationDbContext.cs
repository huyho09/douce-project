using Microsoft.EntityFrameworkCore;
using ScentifyWebApp.Models.Entities;
using System.Collections.Generic;

namespace ScentifyWebApp.DAL.DB
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Perfume> Perfume { get; set; }
        public DbSet<Invoice> Invoice { get; set; }
	}
}
