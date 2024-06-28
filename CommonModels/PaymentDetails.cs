using System.ComponentModel.DataAnnotations;

namespace CommonModels;

public class PaymentDetails
{
    [Key]
    public int Id { get; set; }
    public int PaymentId { get; set; }
    public double Payment { get; set; }
}