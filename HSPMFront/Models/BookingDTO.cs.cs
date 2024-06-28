using System;
using System.Collections.Generic;

namespace CommonModels.DTOs
{
    public class BookingDTO
    {
        public int Id { get; set; }
        public string? UserId { get; set; }
        public PatientDetailsDTO Patient { get; set; }
        public ConsultantDetailsDTO Consultant { get; set; }
        public FacilityDetailsDTO Facility { get; set; }
        public PaymentDetailsDTO Payment { get; set; }
        public AppointmentDetailsDTO Appointment { get; set; }
    }

    public class PatientDetailsDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
    }

    public class ConsultantDetailsDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Specialty { get; set; }
    }

    public class FacilityDetailsDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
    }

    public class PaymentDetailsDTO
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string? PaymentMethod { get; set; }
    }

    public class AppointmentDetailsDTO
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string? Time { get; set; }
        public string? Status { get; set; }
    }
}
