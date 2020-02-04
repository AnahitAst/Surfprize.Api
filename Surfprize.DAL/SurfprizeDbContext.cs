using Microsoft.EntityFrameworkCore;
using Surfprize.DAL.Configuration;
using Surfprize.Entity;

namespace Surfprize.DAL
{
    public class SurfprizeDbContext : DbContext
    {
        public SurfprizeDbContext(DbContextOptions<SurfprizeDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new UserConfiguration());
        }
    }
}
