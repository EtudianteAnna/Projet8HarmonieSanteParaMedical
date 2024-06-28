namespace CommonModels
{
    public class BookingModel
    {
      
        public string? UserId;

        public int Id { get; set; }
        public PatientDetails Patient { get; set; }
        public ConsultantDetails Consultant { get; set; }
        public FacilityDetails Facility { get; set; }
        public PaymentDetails Payment { get; set; }
        public AppointmentDetails Appointment { get; set; }
    }
}

