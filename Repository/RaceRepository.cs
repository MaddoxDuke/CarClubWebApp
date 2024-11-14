using Microsoft.EntityFrameworkCore;
using CarClubWebApp.Data;
using CarClubWebApp.Interfaces;
using CarClubWebApp.Models;

namespace CarClubWebApp.Repository
{
    public class RaceRepository : IRaceRepository
    {
        private readonly ApplicationDbContext _context;
        public RaceRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool Add(Event race)
        {
            _context.Add(race);
            return Save();
        }

        public bool Delete(Event race)
        {
            _context.Remove(race);
            return Save();
        }

        public async Task<IEnumerable<Event>> GetAll()
        {
            return await _context.Races.ToListAsync();
        }

        public async Task<IEnumerable<Event>> GetAllRacesByCity(string city)
        {
            return await _context.Races.Where(c => c.Address.City.Contains(city)).ToListAsync();
        }

        public async Task<Event> GetByIdAsync(int id)
        {
            return await _context.Races.Include(i => i.Address).FirstOrDefaultAsync(i => i.Id == id);
        }
        public async Task<Event> GetByIdAsyncNoTracking(int id)
        {
            return await _context.Races.Include(i => i.Address).AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }

        public bool Update(Event race)
        {
            _context.Update(race);
            return Save();
        }
    }
}
