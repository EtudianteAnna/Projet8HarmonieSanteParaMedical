using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CommonModels.DTOs
{
    public class ConsultantDTO
    {
        public int Id { get; set; }
        public string? Fname { get; set; }
        public string? Lname { get; set; }
        public string? Speciality { get; set; }
        public string? ConsultantName { get; set; }
        public List<DateTime>? AvailableDates { get; set; }

        // Constructeur par défaut
        public ConsultantDTO() { }

        [JsonConstructor]
        public ConsultantDTO(int id, string? fname, string? lname, string? speciality, string? consultantName, List<DateTime>? availableDates)
        {
            Id = id;
            Fname = fname;
            Lname = lname;
            Speciality = speciality;
            ConsultantName = consultantName;
            AvailableDates = availableDates;
        }

        // Constructeur surchargé pour les scénarios sans dates disponibles.
        [JsonConstructor]
        public ConsultantDTO(int id, string? fname, string? lname, string? speciality, string? consultantName, string v, List<DateTime> dateTimes)
            : this(id, fname, lname, speciality, consultantName, new List<DateTime>())
        {
        }
    }
}
