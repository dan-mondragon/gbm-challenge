using Challenge.Data;
using Microsoft.EntityFrameworkCore;
using System;
using Xunit;

namespace Challenge.UnitTest
{
    public class LocationRepositoryTest
    {
        private readonly ChallengeContext _context;

        public LocationRepositoryTest()
        {
            var options = new DbContextOptionsBuilder<ChallengeContext>()
                .UseInMemoryDatabase("TestDB")
                .Options;
            _context = new ChallengeContext(options);
        }

        [Fact]
        public async void Add_ValidObject_CreatesRecord()
        {
            var locationRepository = new Challenge.Data.Repositories.LocationRepository(_context);

            var initial = await locationRepository.GetHistoricalByVehicleIdAsync(1, DateTime.MinValue, DateTime.MaxValue);
            Assert.Equal(0, (int)initial.Count);

            await locationRepository.AddAsync(new Domain.Entities.Location() { Latitude = 19.433410, Longitude = -99.196537, VehicleId = 1, LocationId = 1, CreatedAt = DateTime.Now });
            var locations = await locationRepository.GetHistoricalByVehicleIdAsync(1, DateTime.MinValue, DateTime.MaxValue);
            Assert.Equal(1, (int)locations.Count);
        }

        [Fact]
        public async void Add_ValidObjects_CreatesRecords()
        {
            var locationRepository = new Challenge.Data.Repositories.LocationRepository(_context);
            await locationRepository.AddAsync(new Domain.Entities.Location() { Latitude = 19.433410, Longitude = -99.196537, VehicleId = 1, LocationId = 2, CreatedAt = DateTime.Now });
            await locationRepository.AddAsync(new Domain.Entities.Location() { Latitude = 19.433410, Longitude = -99.196537, VehicleId = 1, LocationId = 3, CreatedAt = DateTime.Now });
            await locationRepository.AddAsync(new Domain.Entities.Location() { Latitude = 19.433410, Longitude = -99.196537, VehicleId = 2, LocationId = 4, CreatedAt = DateTime.Now });

            var locations = await locationRepository.GetHistoricalByVehicleIdAsync(1, DateTime.MinValue, DateTime.MaxValue);
            Assert.Equal(3, locations.Count);
            locations = await locationRepository.GetHistoricalByVehicleIdAsync(2, DateTime.MinValue, DateTime.MaxValue);
            Assert.Single(locations);
        }
    }
}
