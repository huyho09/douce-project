using Microsoft.EntityFrameworkCore;
using ScentifyAdmin.Models.Entities;
using System.Collections.Generic;

namespace ScentifyAdmin.DAL.DB
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Perfume> Perfume { get; set; }
    }
}
