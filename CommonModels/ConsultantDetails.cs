using System.ComponentModel.DataAnnotations;

namespace CommonModels;

public class ConsultantDetails
{
    [Key]
    public int Id { get; set; }
    public int ConsultantId { get; set; }
    public string ConsultantName { get; set; }
    public string ConsultantSpeciality { get; set; }
    public int FacilityId { get; set; }
}