using System.Collections.Generic;
using System.Security.Claims;
using BookingApi.InterfaceServices;
using BookingAPI.Controllers;
using CommonModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace BookingAPI.Tests
{
    public class BookingControllerTests
    {
        private readonly Mock<IBookingService> _mockBookingService;
        private readonly BookingController _controller;

        public BookingControllerTests()
        {
            _mockBookingService = new Mock<IBookingService>();
            _controller = new BookingController(_mockBookingService.Object);
        }

        private void SetUserRole(string role, string userId = "1")
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Role, role),
                new Claim(ClaimTypes.NameIdentifier, userId)
            };
            var identity = new ClaimsIdentity(claims, "TestAuthType");
            var user = new ClaimsPrincipal(identity);

            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = user }
            };
        }

        [Fact]
        public void GetAllBookings_ReturnsOk_ForAdmin()
        {
            // Arrange
            SetUserRole("Admin");
            var bookings = new List<BookingModel> { new BookingModel { Id = 1, UserId = "1" } };
            _mockBookingService.Setup(service => service.GetAllBookings()).Returns(bookings);

            // Act
            var result = _controller.GetAllBookings();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = okResult.Value as IEnumerable<BookingModel>;
            Assert.NotNull(returnValue);
            Assert.Single(returnValue);
        }

        [Fact]
        public void GetAllBookings_ReturnsOk_ForPatient()
        {
            // Arrange
            SetUserRole("Patient", "1");
            var bookings = new List<BookingModel> { new BookingModel { Id = 1, UserId = "1" } };
            _mockBookingService.Setup(service => service.GetBookingsForPatient("1")).Returns(bookings);

            // Act
            var result = _controller.GetAllBookings();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = okResult.Value as IEnumerable<BookingModel>;
            Assert.NotNull(returnValue);
            Assert.Single(returnValue);
        }

        [Fact]
        public void GetBookingById_ReturnsOk_ForAdmin()
        {
            // Arrange
            SetUserRole("Admin");
            var booking = new BookingModel { Id = 1, UserId = "1" };
            _mockBookingService.Setup(service => service.GetBookingById(1)).Returns(booking);

            // Act
            var result = _controller.GetBookingById(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = okResult.Value as BookingModel;
            Assert.NotNull(returnValue);
            Assert.Equal(1, returnValue.Id);
        }

        [Fact]
        public void GetBookingById_ReturnsOk_ForPatient_OwnBooking()
        {
            // Arrange
            SetUserRole("Patient", "1");
            var booking = new BookingModel { Id = 1, UserId = "1" };
            _mockBookingService.Setup(service => service.GetBookingById(1)).Returns(booking);

            // Act
            var result = _controller.GetBookingById(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = okResult.Value as BookingModel;
            Assert.NotNull(returnValue);
            Assert.Equal(1, returnValue.Id);
        }

        [Fact]
        public void GetBookingById_ReturnsForbid_ForPatient_NotOwnBooking()
        {
            // Arrange
            SetUserRole("Patient", "1");
            var booking = new BookingModel { Id = 1, UserId = "2" };
            _mockBookingService.Setup(service => service.GetBookingById(1)).Returns(booking);

            // Act
            var result = _controller.GetBookingById(1);

            // Assert
            Assert.IsType<ForbidResult>(result.Result);
        }

        [Fact]
        public void CreateBooking_ReturnsCreated_ForPatient()
        {
            // Arrange
            SetUserRole("Patient", "1");
            var booking = new BookingModel { Id = 1, UserId = "1" };

            // Act
            var result = _controller.CreateBooking(booking);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var returnValue = createdAtActionResult.Value as BookingModel;
            Assert.NotNull(returnValue);
            Assert.Equal(1, returnValue.Id);
        }

        [Fact]
        public void UpdateBooking_ReturnsNoContent_ForAdmin()
        {
            // Arrange
            SetUserRole("Admin");
            var booking = new BookingModel { Id = 1, UserId = "1" };

            // Act
            var result = _controller.UpdateBooking(1, booking);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void UpdateBooking_ReturnsBadRequest_WhenIdMismatch()
        {
            // Arrange
            SetUserRole("Admin");
            var booking = new BookingModel { Id = 1, UserId = "1" };

            // Act
            var result = _controller.UpdateBooking(2, booking);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public void DeleteBooking_ReturnsNoContent_ForAdmin()
        {
            // Arrange
            SetUserRole("Admin");

            // Act
            var result = _controller.DeleteBooking(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }
    }
}
