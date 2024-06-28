using CommonModels;

namespace CommonModels.Models;

public class ConsultantModelList
{
    public List<Consultant> Consultants { get; set; }

    public ConsultantModelList(List<Consultant> consultants)
    {
        Consultants = consultants ?? throw new ArgumentNullException(nameof(consultants));
    }
}