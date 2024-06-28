using System;
using System.Collections.Generic;
using System.Linq;
using BookingAPI.Services;
using CommonModels;
using Xunit;

namespace BookingAPI.Tests
{
    public class BookingServiceTests
    {
        private readonly BookingService _bookingService;

        public BookingServiceTests()
        {
            _bookingService = new BookingService();
        }

        [Fact]
        public void GetAllBookings_ReturnsEmptyList_WhenNoBookingsExist()
        {
            // Act
            var result = _bookingService.GetAllBookings();

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public void GetAllBookings_ReturnsAllBookings()
        {
            // Arrange
            var booking1 = new BookingModel
            {
                Patient = new PatientDetails { PatientName = "Patient1" },
                Consultant = new ConsultantDetails { ConsultantName = "Consultant1" },
                Facility = new FacilityDetails { FacilityName = "Facility1" },
                Payment = new PaymentDetails { PaymentId = 100 },
                Appointment = new AppointmentDetails { AppointmentDate = DateTime.Now }
            };
            var booking2 = new BookingModel
            {
                Patient = new PatientDetails { PatientName = "Patient2" },
                Consultant = new ConsultantDetails { ConsultantName = "Consultant2" },
                Facility = new FacilityDetails { FacilityName = "Facility2" },
                Payment = new PaymentDetails { PaymentId = 200 },
                Appointment = new AppointmentDetails { AppointmentDate = DateTime.Now }
            };
            _bookingService.CreateBooking(booking1);
            _bookingService.CreateBooking(booking2);

            // Act
            var result = _bookingService.GetAllBookings();

            // Assert
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public void GetBookingById_ReturnsBooking_WhenBookingExists()
        {
            // Arrange
            var booking = new BookingModel
            {
                Patient = new PatientDetails { PatientName = "Patient1" },
                Consultant = new ConsultantDetails { ConsultantName = "Consultant1" },
                Facility = new FacilityDetails { FacilityName = "Facility1" },
                Payment = new PaymentDetails { PaymentId = 100 },
                Appointment = new AppointmentDetails { AppointmentDate = DateTime.Now }
            };
            var createdBooking = _bookingService.CreateBooking(booking);

            // Act
            var result = _bookingService.GetBookingById(createdBooking.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(createdBooking.Id, result.Id);
        }

        [Fact]
        public void GetBookingById_ReturnsNull_WhenBookingDoesNotExist()
        {
            // Act
            var result = _bookingService.GetBookingById(999);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void CreateBooking_ReturnsCreatedBooking_WithValidId()
        {
            // Arrange
            var booking = new BookingModel
            {
                Patient = new PatientDetails { PatientName = "Patient1" },
                Consultant = new ConsultantDetails { ConsultantName = "Consultant1" },
                Facility = new FacilityDetails { FacilityName = "Facility1" },
                Payment = new PaymentDetails { PaymentId = 100 },
                Appointment = new AppointmentDetails { AppointmentTime = DateTime.Now }
            };

            // Act
            var result = _bookingService.CreateBooking(booking);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
        }

        [Fact]
        public void UpdateBooking_ReturnsTrue_WhenBookingExists()
        {
            // Arrange
            var booking = new BookingModel
            {
                Patient = new PatientDetails { PatientName = "Patient1" },
                Consultant = new ConsultantDetails { ConsultantName = "Consultant1" },
                Facility = new FacilityDetails { FacilityName = "Facility1" },
                Payment = new PaymentDetails { PaymentId = 100 },
                Appointment = new AppointmentDetails { AppointmentTime = DateTime.Now }
            };
            var createdBooking = _bookingService.CreateBooking(booking);
            createdBooking.Patient.PatientName = "UpdatedPatient";

            // Act
            var result = _bookingService.UpdateBooking(createdBooking);

            // Assert
            Assert.True(result);
            var updatedBooking = _bookingService.GetBookingById(createdBooking.Id);
            Assert.Equal("UpdatedPatient", updatedBooking.Patient.PatientName);
        }


        [Fact]
        public void UpdateBooking_ReturnsFalse_WhenBookingDoesNotExist()
        {
            // Arrange
            var booking = new BookingModel
            {
                Id = 999,
                Patient = new PatientDetails { PatientName = "NonExistentPatient" },
                Consultant = new ConsultantDetails { ConsultantName = "Consultant1" },
                Facility = new FacilityDetails { FacilityName = "Facility1" },
                Payment = new PaymentDetails { PaymentId = 100 },
                Appointment = new AppointmentDetails { AppointmentTime = DateTime.Now }
            };

            // Act
            var result = _bookingService.UpdateBooking(booking);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void DeleteBooking_ReturnsTrue_WhenBookingExists()
        {
            // Arrange
            var booking = new BookingModel
            {
                Patient = new PatientDetails { PatientName = "Patient1" },
                Consultant = new ConsultantDetails { ConsultantName = "Consultant1" },
                Facility = new FacilityDetails { FacilityName = "Facility1" },
                Payment = new PaymentDetails { PaymentId = 100 },
                Appointment = new AppointmentDetails { AppointmentTime = DateTime.Now }
            };
            var createdBooking = _bookingService.CreateBooking(booking);

            // Act
            var result = _bookingService.DeleteBooking(createdBooking.Id);

            // Assert
            Assert.True(result);
            Assert.Null(_bookingService.GetBookingById(createdBooking.Id));
        }

        [Fact]
        public void DeleteBooking_ReturnsFalse_WhenBookingDoesNotExist()
        {
            // Act
            var result = _bookingService.DeleteBooking(999);

            // Assert
            Assert.False(result);
        }
    }
}
