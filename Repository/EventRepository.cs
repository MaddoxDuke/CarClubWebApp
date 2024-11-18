using Microsoft.EntityFrameworkCore;
using CarClubWebApp.Data;
using CarClubWebApp.Interfaces;
using CarClubWebApp.Models;

namespace CarClubWebApp.Repository
{
    public class EventRepository : IEventRepository
    {
        private readonly ApplicationDbContext _context;
        public EventRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool Add(CarEvent carEvent)
        {
            _context.Add(carEvent);
            return Save();
        }

        public bool Delete(CarEvent carEvent)
        {
            _context.Remove(carEvent);
            return Save();
        }

        public async Task<IEnumerable<CarEvent>> GetAll()
        {
            return await _context.carEvents.ToListAsync();
        }

        public async Task<IEnumerable<CarEvent>> GetAllEventsByCity(string city)
        {
            return await _context.carEvents.Where(c => c.Address.City.Contains(city)).ToListAsync();
        }

        public async Task<CarEvent> GetByIdAsync(int id)
        {
            return await _context.carEvents.Include(i => i.Address).FirstOrDefaultAsync(i => i.Id == id);
        }
        public async Task<CarEvent> GetByIdAsyncNoTracking(int id)
        {
            return await _context.carEvents.Include(i => i.Address).AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }

        public bool Update(CarEvent carEvent)
        {
            _context.Update(carEvent);
            return Save();
        }
    }
}
