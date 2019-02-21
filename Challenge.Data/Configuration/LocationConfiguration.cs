using Challenge.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Challenge.Data.Configuration
{
    public class LocationConfiguration
    {
        /// <summary>
        /// Vehicle Entity Configuration
        /// </summary>
        /// <param name="entity"></param>
        public LocationConfiguration(EntityTypeBuilder<Location> entity)
        {
            entity.Property(e => e.LocationId).UseSqlServerIdentityColumn();
            entity.Property(e => e.Latitude).IsRequired();
            entity.Property(e => e.Longitude).IsRequired();

            entity.HasOne(e => e.Vehicle).WithMany(e => e.Locations).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
