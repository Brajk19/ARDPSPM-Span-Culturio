using Microsoft.EntityFrameworkCore;
using Span.Culturio.Api.Data.Entity;
using System.Reflection;

namespace Span.Culturio.Api.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base (options) 
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<CultureObject> CultureObjects { get; set; }
        public DbSet<Package> Packages { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
