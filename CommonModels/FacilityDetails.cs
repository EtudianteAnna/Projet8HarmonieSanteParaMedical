using System.ComponentModel.DataAnnotations;

namespace CommonModels;

public class FacilityDetails
{
    [Key]
    public int Id { get; set; }
    public int FacilityId { get; set; }
    public string FacilityName { get; set; }
    public string FacilityAddressLine1 { get; set; }
    public string FacilityAddressLine2 { get; set; }
    public string FacilityRegion { get; set; }
    public string FacilityCity { get; set; }
    public string FacilityPostcode { get; set; }
    public string FacilityContactNumber { get; set; }
}