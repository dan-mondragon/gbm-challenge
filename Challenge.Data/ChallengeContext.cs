using Challenge.Data.Configuration;
using Challenge.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Challenge.Data
{
    public class ChallengeContext : DbContext
    {
        public static readonly Microsoft.Extensions.Logging.LoggerFactory _loggerFactory = new LoggerFactory(new[] { new Microsoft.Extensions.Logging.Debug.DebugLoggerProvider() });
        public virtual DbSet<Vehicle> Vehicle { get; set; }
        public virtual DbSet<Location> Location { get; set; }
        public virtual DbSet<User> User { get; set; }

        private readonly string _dbName;

        public ChallengeContext(DbContextOptions<ChallengeContext> options) : base(options)
        {
        }

        public ChallengeContext(string dbName)
        {
            _dbName = dbName;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseLoggerFactory(_loggerFactory);

            if (!string.IsNullOrEmpty(_dbName))
                optionsBuilder.UseSqlServer(_dbName);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new VehicleConfiguration(modelBuilder.Entity<Vehicle>());
            new LocationConfiguration(modelBuilder.Entity<Location>());
            new UserConfiguration(modelBuilder.Entity<User>());
        }
    }
}
