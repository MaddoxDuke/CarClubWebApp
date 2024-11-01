using Microsoft.AspNetCore.Mvc;
using RunGroupWebApp.Data;
using RunGroupWebApp.Interfaces;
using RunGroupWebApp.Models;
using RunGroupWebApp.Repository;
using RunGroupWebApp.Services;
using RunGroupWebApp.ViewModels;

namespace RunGroupWebApp.Controllers
{
    public class RaceController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IRaceRepository _raceRepository;
        private readonly IPhotoService _photoService;
        public RaceController(ApplicationDbContext context, IRaceRepository raceRepository, IPhotoService photoService)
        {
            // when you use context think of Db. Brings tables from Db into program.
            _raceRepository = raceRepository;
            _photoService = photoService;
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
        //NEED This for url to show...
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateRaceViewModel raceVM)
        {
            if (ModelState.IsValid)
            {
                var result = await _photoService.AddPhotoAsync(raceVM.Image);

                var race = new Race
                {
                    Title = raceVM.Title,
                    Description = raceVM.Description,
                    Image = result.Url.ToString(),
                    Address = new Address
                    {
                        Street = raceVM.Address.Street,
                        City = raceVM.Address.City,
                        State = raceVM.Address.State
                    }
                };

                _raceRepository.Add(race);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Photo upload failed");
            }
            return View(raceVM);
        }
    }
}
