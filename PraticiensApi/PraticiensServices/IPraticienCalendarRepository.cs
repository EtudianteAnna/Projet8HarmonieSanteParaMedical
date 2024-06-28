using CommonModels;
using System.Collections.Generic;

namespace Praticiens.Services
{
    public interface IPraticienCalendarRepository
    {
        IEnumerable<PraticienCalendars> GetAllCalendars();
        PraticienCalendars GetCalendarById(int id);
        void CreateCalendar(PraticienCalendars calendar);
        void UpdateCalendar(PraticienCalendars calendar);
        void DeleteCalendar(int id);
    }
}
