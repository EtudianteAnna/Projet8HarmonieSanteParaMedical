namespace CommonModels
{
    public class PraticienCalendars
    {
        public int Id { get; set; }
        public string PraticienId { get; set; } // ID du praticien
        public DateTime AppointmentDate { get; set; } // Date de l'agenda
        public string Notes { get; set; } // Notes ou détails supplémentaires
    }
}