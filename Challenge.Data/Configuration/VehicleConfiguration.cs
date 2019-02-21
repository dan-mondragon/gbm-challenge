using Challenge.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Challenge.Data.Configuration
{
    public class VehicleConfiguration
    {
        /// <summary>
        /// Vehicle Entity Configuration
        /// </summary>
        /// <param name="entity"></param>
        public VehicleConfiguration(EntityTypeBuilder<Vehicle> entity)
        {
            entity.Property(e => e.VehicleId).UseSqlServerIdentityColumn();
            entity.Property(e => e.Plate).HasMaxLength(10);

            entity.HasMany(e => e.Locations).WithOne(e => e.Vehicle).OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(e => e.User).WithMany(e => e.Vehicles).IsRequired().OnDelete(DeleteBehavior.Restrict);

            entity.HasData(
                new Vehicle() { VehicleId = 1, Plate = "ABC123", UserId = 1, CreatedAt = DateTime.Now },
                new Vehicle() { VehicleId = 2, Plate = "EFG456", UserId = 1, CreatedAt = DateTime.Now },
                new Vehicle() { VehicleId = 3, Plate = "HIJ789", UserId = 1, CreatedAt = DateTime.Now }
            );
        }
    }
}
