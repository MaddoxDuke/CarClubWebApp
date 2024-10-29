using Microsoft.AspNetCore.Mvc;
using RunGroupWebApp.Data;
using RunGroupWebApp.Interfaces;
using RunGroupWebApp.Models;

namespace RunGroupWebApp.Controllers
{
    public class ClubController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IClubRepository _clubRepository;
        public ClubController(ApplicationDbContext context, IClubRepository clubRepository)
        {
            // when you use context think of Db. Brings tables from Db into program.
            _clubRepository = clubRepository;
        }
        // MVC description
        public async Task<IActionResult> Index() //C
        {
            IEnumerable<Club> clubs = await _clubRepository.GetAll(); //M: Db to clubs and builsd query/db and brings back.
            return View(clubs); //V
        }
        public async Task<IActionResult> Detail(int id) //Detail pages do not return lists of Club. Just one
        {
            Club club = await _clubRepository.GetByIdAsync(id);
            return View(club);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Club club) 
        {
            if (!ModelState.IsValid)
            {
                return View(club);
            }
            _clubRepository.Add(club);
            return RedirectToAction("Index");
        }
    }
}
