using System.ComponentModel.DataAnnotations;

namespace CommonModels;

public class ConsultantModel
{
    [Key]
    public int Id { get; set; }
    public string Fname { get; set; }
    public string Lname { get; set; }
    public string Speciality { get; set; }
    public List<ConsultantCalendarModel> ConsultantCalendars { get; set; }
    public int SelectedConsultantId { get; set; }
    public Microsoft.AspNetCore.Mvc.Rendering.SelectList ConsultantsList { get; set; }
}