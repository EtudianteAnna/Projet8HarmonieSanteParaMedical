using System.ComponentModel.DataAnnotations;

namespace CommonModels;

public class ConsultantCalendarModel
{
    [Key]
    public int Id { get; set; }
    public string ConsultantName { get; set; }
    public List<DateTime> AvailableDates { get; set; }
}