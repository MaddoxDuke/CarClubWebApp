using CarClubWebApp.Models;
// Repositories can also be called...

namespace CarClubWebApp.Interfaces
{
    public interface IClubRepository
    {
        Task<IEnumerable<Club>> GetAll();
        Task<Club> GetByIdAsync(int id);
        Task<Club> GetByIdAsyncNoTracking(int id);

        Task<IEnumerable<Club>> GetClubByCity(string city);
        //Will always have the Add, update, delete, and saves here. Design structure
        bool Add(Club club);
        bool Update(Club club);
        bool Delete(Club club);
        bool Save();
    }
}
