using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RunGroupWebApp.Data;
using RunGroupWebApp.Models;

namespace RunGroupWebApp.Controllers
{
    public class RaceController : Controller
    {
        private readonly ApplicationDbContext _context;
        public RaceController(ApplicationDbContext context)
        {
            _context = context; // when yo usee context think of Db. Brings tables from Db into program.
        }
        // MVC description
        public IActionResult Index() //C
        {
            var races = _context.Races.ToList(); //M: Db to clubs and builsd query/db and brings back.
            return View(races); //V
        }
        public IActionResult Detail(int id) //Detail pages do not return lists of Club. Just one
        {
            Race race = _context.Races.Include(a => a.Address).FirstOrDefault(c => c.Id == id);
            return View(race);
        }
    }
}
