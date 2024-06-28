using System.ComponentModel.DataAnnotations;

namespace CommonModels;

public class AppointmentDetails
{
    [Key]
    public int Id { get; set; }
    public Guid AppointmentId { get; set; }
    public DateTime AppointmentDate { get; set; }
    public DateTime AppointmentTime { get; set; }
}