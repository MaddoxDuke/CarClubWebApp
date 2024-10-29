using Microsoft.AspNetCore.Mvc;
using RunGroupWebApp.Data;
using RunGroupWebApp.Interfaces;
using RunGroupWebApp.Models;

namespace RunGroupWebApp.Controllers
{
    public class RaceController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IRaceRepository _raceRepository;
        public RaceController(ApplicationDbContext context, IRaceRepository raceRepository)
        {
            // when you use context think of Db. Brings tables from Db into program.
            _raceRepository = raceRepository;
        }
        // MVC description
        public async Task<IActionResult> Index() //C
        {
            IEnumerable<Race> races = await _raceRepository.GetAll(); //M: Db to clubs and builsd query/db and brings back.
            return View(races); //V
        }
        public async Task<IActionResult> Detail(int id) //Detail pages do not return lists of Club. Just one
        {
            Race race = await _raceRepository.GetByIdAsync(id);
            return View(race);
        }
    }
}
