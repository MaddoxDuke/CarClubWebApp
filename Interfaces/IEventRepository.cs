using CarClubWebApp.Models;

namespace CarClubWebApp.Interfaces
{
    public interface IEventRepository
    {
        Task<IEnumerable<CarEvent>> GetAll();
        Task<CarEvent> GetByIdAsync(int id);
        Task<CarEvent> GetByIdAsyncNoTracking(int id);

        Task<IEnumerable<CarEvent>> GetAllEventsByCity(string city);
        //Will always have the Add, update, delete, and saves here. Design structure
        bool Add(CarEvent carEvent);
        bool Update(CarEvent carEvent);
        bool Delete(CarEvent carEvent);
        bool Save();
    }
}
