using Microsoft.EntityFrameworkCore;
using RunGroupWebApp.Data;
using RunGroupWebApp.Interfaces;
using RunGroupWebApp.Models;

namespace RunGroupWebApp.Repository
{
    public class ClubRepository : IClubRepository
    {
        private readonly ApplicationDbContext _context;
        public ClubRepository(ApplicationDbContext context) 
        {
            _context = context;
        }
        public bool Add(Club club)
        {
            _context.Add(club); //generates sql and does not actually add until save().
            return Save();
        }

        public bool Delete(Club club)
        {
            _context.Remove(club);
            return Save();
        }

        public async Task<IEnumerable<Club>> GetAll()
        {
            // returns a list. Reason is because it is generating a page
            return await _context.Clubs.ToListAsync();
            
        }

        public async Task<Club> GetByIdAsync(int id) 
        {
            //returns an individual id, NOT a list. Is not generating a full page, just the details.
            return await _context.Clubs.Include(i => i.Address).FirstOrDefaultAsync(i => i.Id == id);
        }
        public async Task<Club> GetByIdAsyncNoTracking(int id)
        {
            //returns an individual id, NOT a list. Is not generating a full page, just the details.
            return await _context.Clubs.Include(i => i.Address).AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<IEnumerable<Club>> GetClubByCity(string city)
        {
            return await _context.Clubs.Where(c => c.Address.City.Contains(city)).ToListAsync();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }

        public bool Update(Club club)
        {
            _context.Update(club);
            return Save();
        }
    }
}
