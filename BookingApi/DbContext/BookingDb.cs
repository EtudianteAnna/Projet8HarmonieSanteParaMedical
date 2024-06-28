using Microsoft.EntityFrameworkCore;
using CommonModels;

namespace BookingApi.DbContext
{
    public class BookingDb : Microsoft.EntityFrameworkCore.DbContext
    {
        public BookingDb(DbContextOptions options) : base(options) { }

        public DbSet<BookingModel?> Bookings { get; set; }
        public DbSet<PatientDetails> Patients { get; set; }
        public DbSet<ConsultantDetails> Consultants { get; set; }
        public DbSet<FacilityDetails> Facilities { get; set; }
        public DbSet<PaymentDetails> Payments { get; set; }
        public DbSet<AppointmentDetails> Appointments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Définir les clés primaires si elles ne sont pas définies par les attributs
            modelBuilder.Entity<ConsultantDetails>().HasKey(e => e.Id);
            modelBuilder.Entity<PatientDetails>().HasKey(e => e.Id);
            modelBuilder.Entity<FacilityDetails>().HasKey(e => e.Id);
            modelBuilder.Entity<PaymentDetails>().HasKey(e => e.Id);
            modelBuilder.Entity<AppointmentDetails>().HasKey(e => e.Id);
        }

        internal void SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}