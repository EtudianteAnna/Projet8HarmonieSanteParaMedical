using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CommonModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Praticiens.Services;
using PraticiensApi.Controllers;
using Xunit;

namespace PraticiensApi.Tests
{
    public class PraticienCalendarControllerTests
    {
        private readonly Mock<IPraticienCalendarRepository> _mockRepo;
        private readonly PraticienCalendarController _controller;

        public PraticienCalendarControllerTests()
        {
            _mockRepo = new Mock<IPraticienCalendarRepository>();
            _controller = new PraticienCalendarController(_mockRepo.Object);
        }

        [Fact]
        public void GetAllCalendarsReturnsOkResult()
        {
            // Arrange
            var mockCalendars = new List<PraticienCalendars>
            {
                new PraticienCalendars
                {
                    Id = 1,
                    PraticienId = "praticien-123",
                    AppointmentDate = DateTime.Now,
                    Notes = "Test Notes"
                }
            };
            _mockRepo.Setup(repo => repo.GetAllCalendars()).Returns(mockCalendars);

            // Act
            var result = _controller.GetAllCalendars();

            // Assert
            var actionResult = Assert.IsType<ActionResult<IEnumerable<PraticienCalendars>>>(result);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var returnValue = Assert.IsType<List<PraticienCalendars>>(okResult.Value);
            Assert.Single(returnValue);
        }

        [Fact]
        public void GetCalendarById_ReturnsOkResult_WhenCalendarExists()
        {
            // Arrange
            var mockCalendar = new PraticienCalendars
            {
                Id = 1,
                PraticienId = "praticien-123",
                AppointmentDate = DateTime.Now,
                Notes = "Test Notes"
            };
            _mockRepo.Setup(repo => repo.GetCalendarById(1)).Returns(mockCalendar);

            // Act
            var result = _controller.GetCalendarById(1);

            // Assert
            var actionResult = Assert.IsType<ActionResult<PraticienCalendars>>(result);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var returnValue = Assert.IsType<PraticienCalendars>(okResult.Value);
            Assert.Equal(1, returnValue.Id); ;
        }

        [Fact]
        public void GetCalendarById_ReturnsNotFound_WhenCalendarDoesNotExist()
        {
            // Arrange
            _mockRepo.Setup(repo => repo.GetCalendarById(It.IsAny<int>())).Returns((PraticienCalendars)null);

            // Act
            var result = _controller.GetCalendarById(999);

            // Assert
            var actionResult = Assert.IsType<ActionResult<PraticienCalendars>>(result);
            Assert.IsType<NotFoundResult>(actionResult.Result);
        }

        [Fact]
        public void CreateCalendar_ReturnsCreatedResponse()
        {
            // Arrange
            var newCalendar = new PraticienCalendars
            {
                Id = 2,
                PraticienId = "praticien-456",
                AppointmentDate = DateTime.Now,
                Notes = "New Calendar Notes"
            };

            // Act
            var result = _controller.CreateCalendar(newCalendar);

            // Assert
            var createdResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status201Created, createdResult.StatusCode);
            var returnValue = Assert.IsType<PraticienCalendars>(createdResult.Value);
            Assert.Equal(2, returnValue.Id);
        }

        [Fact]
        public void CreateCalendar_ReturnsBadRequest_WhenModelIsInvalid()
        {
            // Arrange
            _controller.ModelState.AddModelError("error", "some error");

            // Act
            var result = _controller.CreateCalendar(null);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Le corps de la requête ne peut pas être null.", badRequestResult.Value);
        }

        [Fact]
        public void DeleteCalendar_ReturnsNoContent()
        {
            // Act
            var result = _controller.DeleteCalendar(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }
    }
}
