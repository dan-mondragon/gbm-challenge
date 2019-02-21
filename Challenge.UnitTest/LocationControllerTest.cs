using Challenge.Api.Controllers;
using Challenge.Domain.Managers;
using Challenge.Domain.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Challenge.UnitTest
{
    public class LocationControllerTest
    {
        LocationController _controller;
        ILocationManager _manager;

        public LocationControllerTest()
        {
            var mock = new Mock<ILocationManager>();
            var vehicleMock = new Mock<IVehicleManager>();

            mock.Setup(m => m.GetHistoricalLocationByVehicleIdAsync(1, DateTime.MinValue, DateTime.MaxValue, default(CancellationToken))).Returns(() => {
                var vmList = new List<LocationViewModel>();
                vmList.Add(new LocationViewModel());
                vmList.Add(new LocationViewModel());
                return Task.FromResult(vmList);
            });
            mock.Setup(m => m.GetLocationByVehicleIdAsync(1, default(CancellationToken))).Returns(Task.FromResult(new LocationViewModel()));
            vehicleMock.Setup(v => v.isValidUserVehicle(1, It.IsAny<int>(), default(CancellationToken))).Returns(Task.FromResult(true));
            _controller = new LocationController(mock.Object, vehicleMock.Object);
        }

        [Fact]
        public async void Add_ValidObjectPassed_ReturnsCreatedResponse()
        {
            // Arrange
            LocationRequestViewModel testItem = new LocationRequestViewModel()
            {
                Longitude = 19.433052,
                Latitude = -99.182882,
                VehicleId = 1
            };

            AddClaim(_controller);

            // Act
            var createdResponse = await _controller.Post(testItem);
            var objectResponse = createdResponse as ObjectResult;

            // Assert
            Assert.Equal(201, objectResponse.StatusCode);
        }

        [Fact]
        public async void Add_InvalidObjectPassed_ReturnsBadRequest()
        {
            var testItem = new LocationRequestViewModel()
            {
                Longitude = 19.433052,
                Latitude = -99.182882,
            };

            AddClaim(_controller);

            //Needs ModelState validation in controller.
            _controller.ModelState.AddModelError("VehicleId", "Required");
            
            var badResponse = await _controller.Post(testItem);

            Assert.IsType<BadRequestObjectResult>(badResponse);
        }

        [Fact]
        public async void Add_InvalidNullObjectPassed_ReturnsBadRequest()
        {
            LocationRequestViewModel LocationRequestViewModel = null;

            var badResponse = await _controller.Post(LocationRequestViewModel);

            Assert.IsType<BadRequestResult>(badResponse);
        }

        [Fact]
        public async void GetCurrent_WhenCalled_ReturnsOkResult()
        {
            AddClaim(_controller);

            var okResult = await _controller.GetCurrent(1, default(CancellationToken));

            Assert.IsType<OkObjectResult>(okResult);
        }

        [Fact]
        public void GetHistorical_WhenCalled_ReturnsAllItems()
        {
            AddClaim(_controller);

            var okResult = _controller.GetHistorical(1, DateTime.MinValue, DateTime.MaxValue, default(CancellationToken)).Result as OkObjectResult;

            var items = Assert.IsType<List<LocationViewModel>>(okResult.Value);
            Assert.Equal(2, items.Count);
        }

        [Fact]
        public void Get_UnknownIdPassed_ReturnsNotFoundResult()
        {
            AddClaim(_controller);
            var notFoundResult = _controller.GetCurrent(-10);
            Assert.IsType<NotFoundResult>(notFoundResult.Result);
        }

        private void AddClaim(LocationController _controller)
        {
            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
                    { 
                        new Claim(ClaimTypes.Name, "1")
                    }, "Auth"))
                }
            };
        }
    }
}
