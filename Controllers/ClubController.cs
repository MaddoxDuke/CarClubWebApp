using Microsoft.AspNetCore.Mvc;
using RunGroupWebApp.Data;
using RunGroupWebApp.Interfaces;
using RunGroupWebApp.Models;
using RunGroupWebApp.ViewModels;

namespace RunGroupWebApp.Controllers
{
    public class ClubController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IClubRepository _clubRepository;
        private readonly IPhotoService _photoService;

        public ClubController(ApplicationDbContext context, IClubRepository clubRepository, IPhotoService photoService)
        {
            // when you use context think of Db. Brings tables from Db into program.
            _clubRepository = clubRepository;
            _photoService = photoService;
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
        public async Task<IActionResult> Create(CreateClubViewModel clubVM) 
        {
            if (ModelState.IsValid)
            {
                var result = await _photoService.AddPhotoAsync(clubVM.Image);

                var club = new Club
                {
                    Title = clubVM.Title,
                    Description = clubVM.Description,
                    Image = result.Url.ToString(),
                    Address = new Address
                    {
                        Street = clubVM.Address.Street,
                        City = clubVM.Address.City,
                        State = clubVM.Address.State
                    }
                };

                _clubRepository.Add(club);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Photo upload failed");
            }
            return View(clubVM);
        }
    }
}
