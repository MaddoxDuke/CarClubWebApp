using Microsoft.AspNetCore.Mvc;
using CarClubWebApp.Data;
using CarClubWebApp.Interfaces;
using CarClubWebApp.Models;
using CarClubWebApp.ViewModels;

namespace CarClubWebApp.Controllers
{
    public class RaceController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IRaceRepository _eventRepository;
        private readonly IPhotoService _photoService;
        public RaceController(ApplicationDbContext context, IRaceRepository eventRepository, IPhotoService photoService)
        {
            // when you use context think of Db. Brings tables from Db into program.
            _eventRepository = eventRepository;
            _photoService = photoService;
            _context = context;
        }
        // MVC description
        public async Task<IActionResult> Index() //C
        {
            IEnumerable<Event> carEvent = await _eventRepository.GetAll(); //M: Db to events and builsd query/db and brings back.
            return View(carEvent); //V
        }
        public async Task<IActionResult> Detail(int id) //Detail pages do not return lists of event. Just one
        {
            Event carEvent = await _eventRepository.GetByIdAsync(id);
            return View(carEvent);
        }
        //NEED This for url to show...
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateRaceViewModel carEventVM)
        {
            if (ModelState.IsValid)
            {
                var result = await _photoService.AddPhotoAsync(carEventVM.Image);

                var carEvent = new Event
                {
                    Title = carEventVM.Title,
                    Description = carEventVM.Description,
                    Image = result.Url.ToString(),
                    Address = new Address
                    {
                        Street = carEventVM.Address.Street,
                        City = carEventVM.Address.City,
                        State = carEventVM.Address.State
                    }
                };

                _eventRepository.Add(carEvent);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Photo upload failed");
            }
            return View(carEventVM);
        }
        public async Task<IActionResult> Edit(int id)
        {
            var carEvent = await _eventRepository.GetByIdAsync(id);

            if (carEvent == null) return View("Error");

            var carEventVM = new EditRaceViewModel
            {
                Title = carEvent.Title,
                Description = carEvent.Description,
                AddressId = carEvent.AddressId,
                Address = carEvent.Address,
                URL = carEvent.Image,
                RaceCategory = carEvent.RaceCategory
            };

            return View(carEventVM);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditRaceViewModel carEventVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit event");
                return View("Edit", carEventVM);
            }
            else
            {

            }

            var userEvent = await _eventRepository.GetByIdAsyncNoTracking(id);

            if (userEvent != null)
            {
                try
                {
                    await _photoService.DeletePhotoAsync(userEvent.Image);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Could not delete photo");
                    return View(carEventVM);
                }
                var photoResult = await _photoService.AddPhotoAsync(carEventVM.Image);
                
                if (photoResult == null || photoResult.Url == null)
                {
                    ModelState.AddModelError("Image", "Photo upload failed.");
                    return View(carEventVM);
                }
                var carEvent = new Event
                {
                    Id = id,
                    Title = carEventVM.Title,
                    Description = carEventVM.Description,
                    Image = photoResult.Url.ToString(),
                    AddressId = carEventVM.AddressId,
                    Address = carEventVM.Address
                };
                _eventRepository.Update(carEvent);

                return RedirectToAction("Index");
            }
            else
            {
                return View(carEventVM);
            }
        }
    }
}
