using CommonModels;
using Praticiens.Services;

namespace PraticiensApi.PraticienRepository
{
    public class PraticienCalendarRepository : IPraticienCalendarRepository
    {
        private readonly List<PraticienCalendars> _calendars = new();

        public PraticienCalendarRepository()
        {
            // Initialisation des données factices en mémoire
            // _calendars = new List<PraticienCalendars>();
        }

        public IEnumerable<PraticienCalendars> GetAllCalendars()
        {
            return _calendars;
        }

        public PraticienCalendars? GetCalendarById(int id)
        {
            return _calendars.FirstOrDefault(c => c.Id == id);
        }

        public void CreateCalendar(PraticienCalendars calendar)
        {
            _calendars.Add(calendar);
        }

        public void UpdateCalendar(PraticienCalendars calendar)
        {
            var existingCalendar = GetCalendarById(calendar.Id);
            if (existingCalendar != null)
            {
                _calendars.Remove(existingCalendar);
                _calendars.Add(calendar);
            }
        }

        public void DeleteCalendar(int id)
        {
            var calendar = GetCalendarById(id);
            if (calendar != null)
            {
                _calendars.Remove(calendar);
            }
        }
    }
}