using Challenge.Domain.Entities;
using Challenge.UnitTest.MockData;
using System;
using System.Collections.Generic;
using Xunit;

namespace Challenge.UnitTest
{
    public class LocationManagerTest
    {
        private readonly LocationRepository _repository;

        public LocationManagerTest()
        {
            _repository = new LocationRepository();
        }

        [Fact]
        public async void Add_ValidObjectPassed_ReturnsObject()
        {
            // Arrange
            var testItem = new Location()
            {
                Latitude = 19.433410,
                Longitude = -99.196537,
                VehicleId = 3
            };

            // Act
            var createdItem = await _repository.AddAsync(testItem);

            // Assert
            Assert.IsType<Location>(createdItem);
        }

        [Fact]
        public async void Add_ValidObjectPassed_ReturnsCreatedItem()
        {
            var testItem = new Location()
            {
                Latitude = 19.433410,
                Longitude = -99.196537,
                VehicleId = 3
            };

            var createdItem = await _repository.AddAsync(testItem);

            Assert.IsType<Location>(createdItem);
            Assert.Equal(19.433410, createdItem.Latitude);
            Assert.Equal(-99.196537, createdItem.Longitude);
        }

        [Fact]
        public async void GetHistorical_UnknownIdPassed_ReturnsEmptyArray()
        {
            var VehicleId = -10;
            var result = await _repository.GetHistoricalByVehicleIdAsync(VehicleId, DateTime.MinValue, DateTime.MaxValue);
            Assert.Empty(result);
        }

        [Fact]
        public async void GetCurrent_UnknownIdPassed_ReturnsNull()
        {
            var VehicleId = -5;
            var result = await _repository.GetCurrentByVehicleIdAsync(VehicleId);
            Assert.Null(result);
        }

        [Fact]
        public async void GetCurrent_ExistingIdPassed_ReturnsObject()
        {
            var VehicleId = 1;
            var result = await _repository.GetCurrentByVehicleIdAsync(VehicleId);
            Assert.IsType<Location>(result);
        }

        [Fact]
        public async void GetHistorical_ExistingIdPassed_ReturnsList()
        {
            var VehicleId = 2;
            var result = await _repository.GetHistoricalByVehicleIdAsync(VehicleId, DateTime.MinValue, DateTime.MaxValue);
            Assert.IsType<List<Location>>(result);
        }

        [Fact]
        public async void GetCurrent_ExistingIdPassed_ReturnsRightItem()
        {
            var VehicleId = 3;

            var result = await _repository.GetCurrentByVehicleIdAsync(VehicleId);

            Assert.IsType<Location>(result);
            Assert.Equal(VehicleId, result.VehicleId);
        }
    }
}
