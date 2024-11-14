using CarClubWebApp.Models;

namespace CarClubWebApp.Interfaces
{
    public interface IRaceRepository
    {
        Task<IEnumerable<Event>> GetAll();
        Task<Event> GetByIdAsync(int id);
        Task<Event> GetByIdAsyncNoTracking(int id);

        Task<IEnumerable<Event>> GetAllRacesByCity(string city);
        //Will always have the Add, update, delete, and saves here. Design structure
        bool Add(Event race);
        bool Update(Event race);
        bool Delete(Event race);
        bool Save();
    }
}
