using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RunGroupWebApp.Data;
using RunGroupWebApp.Models;

namespace RunGroupWebApp.Controllers
{
    public class ClubController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ClubController(ApplicationDbContext context)
        {
            _context = context; // when yo use context think of Db. Brings tables from Db into program.
        }
        // MVC description
        public IActionResult Index() //C
        {
            var clubs = _context.Clubs.ToList(); //M: Db to clubs and builsd query/db and brings back.
            return View(clubs); //V
        }
        public IActionResult Detail(int id) //Detail pages do not return lists of Club. Just one
        {
            Club club = _context.Clubs.Include(a => a.Address).FirstOrDefault(c => c.Id == id);
            return View(club);
        }
    }
}
