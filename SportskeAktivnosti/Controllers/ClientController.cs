using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SportskeAktivnosti.Models;
using SportskeAktivnosti.Models.Repository.Interfaces;
using SportskeAktivnosti.ViewModels;

namespace SportskeAktivnosti.Controllers
{
    [Authorize(Roles = "Admin, Client")]
    public class ClientController : Controller
    {
        private readonly ISportObjectRepository sportObjectRepository;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly UserManager<IdentityUser> userManager;
        private readonly ITournamentRepository tournamentRepository;
        private readonly ISportSchoolRepository sportSchoolRepository;

        public ClientController(ISportObjectRepository sportObjectRepository,
            IWebHostEnvironment webHostEnvironment,
            UserManager<IdentityUser> userManager,
            ITournamentRepository tournamentRepository,
            ISportSchoolRepository sportSchoolRepository)
        {
            this.sportObjectRepository = sportObjectRepository;
            this.webHostEnvironment = webHostEnvironment;
            this.userManager = userManager;
            this.tournamentRepository = tournamentRepository;
            this.sportSchoolRepository = sportSchoolRepository;
        }

        [HttpGet]
        [Authorize(Roles = "Client")]
        public async Task<IActionResult> EditProfile()
        {
            var user = await userManager.FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with ID = {user.Id} cannot be found";
                return View("NotFound");
            }

            var sportObject = sportObjectRepository.GetByEmail(user.Email);
            var tournament = tournamentRepository.GetByEmail(user.Email);
            var sportSchool = sportSchoolRepository.GetByEmail(user.Email);

            if (sportObject != null)
            {
                return RedirectToAction("EditSportObject", new { id = sportObject.Id });
            }
            else if(tournament != null)
            {
                return RedirectToAction("EditTournament", new { id = tournament.Id });
            }
            else if (sportSchool != null)
            {
                return RedirectToAction("EditSportSchool", new { id = sportSchool.Id });
            }

            ViewBag.ErrorMessage = $"Ne posedujete svoj objekat";
            return View("NotFound");
        }


        // ***********************************
        // ************ SPORT OBJECTS ****************
        // ***********************************
        [HttpGet]
        public async Task<IActionResult> EditSportObject(int id)
        {
            SportObject model = sportObjectRepository.Get(id);
            SportObjectEditViewModel sportObjectEditViewModel = new SportObjectEditViewModel
            {
                Id = model.Id,
                Name = model.Name,
                Email = model.Email,
                Address = model.Address,
                CityId = model.CityId,
                WorkEnds = model.WorkEnds,
                WorkStarts = model.WorkStarts,
                Description = model.Description,
                SportId = model.SportId,
                Phone = model.Phone,
                PriceForHour = model.PriceForHour,
                IsPayed = model.IsPayed,
                ExistingImage1Path = model.Image1Path,
                ExistingImage2Path = model.Image2Path,
                Cities = sportObjectRepository.GetAllCities(),
                Sports = sportObjectRepository.GetAllSports()
            };

            if (User.IsInRole("Client"))
            {
                var user = await userManager.FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));
                if (user == null)
                {
                    ViewBag.ErrorMessage = $"User with ID = {user.Id} cannot be found";
                    return View("NotFound");
                }

                if (user.Email != model.Email)
                {
                    return RedirectToAction("AccessDenied", "Administration");
                }
            }

            return View(sportObjectEditViewModel);
        }

        [HttpPost]
        public IActionResult EditSportObject(SportObjectEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                SportObject sportObject = sportObjectRepository.Get(model.Id);
                sportObject.Name = model.Name;
                sportObject.Email = model.Email;
                sportObject.Address = model.Address;
                sportObject.CityId = model.CityId;
                sportObject.WorkEnds = model.WorkEnds;
                sportObject.WorkStarts = model.WorkStarts;
                sportObject.Description = model.Description;
                sportObject.SportId = model.SportId;
                sportObject.Phone = model.Phone;
                sportObject.PriceForHour = model.PriceForHour;
                sportObject.IsPayed = model.IsPayed;

                if (model.Image1 != null)
                {
                    if (model.ExistingImage1Path != null)
                    {
                        string filePath = Path.Combine(webHostEnvironment.WebRootPath, "images", model.ExistingImage1Path);
                        System.IO.File.Delete(filePath);
                    }
                    sportObject.Image1Path = ProcessUploadFile(model.Image1);
                }

                if (model.Image2 != null)
                {
                    if (model.ExistingImage2Path != null)
                    {
                        string filePath = Path.Combine(webHostEnvironment.WebRootPath, "images", model.ExistingImage2Path);
                        System.IO.File.Delete(filePath);
                    }
                    sportObject.Image2Path = ProcessUploadFile(model.Image2);
                }

                sportObjectRepository.Update(sportObject);

                return RedirectToAction("Details", "SportObject", new { id = sportObject.Id });
            }

            //return RedirectToAction("EditSportObject");

            SportObject modelOld = sportObjectRepository.Get(model.Id);
            SportObjectEditViewModel sportObjectEditViewModel = new SportObjectEditViewModel
            {
                Id = modelOld.Id,
                Name = modelOld.Name,
                Email = modelOld.Email,
                Address = modelOld.Address,
                CityId = modelOld.CityId,
                WorkEnds = modelOld.WorkEnds,
                WorkStarts = modelOld.WorkStarts,
                Description = modelOld.Description,
                SportId = modelOld.SportId,
                Phone = modelOld.Phone,
                PriceForHour = modelOld.PriceForHour,
                IsPayed = modelOld.IsPayed,
                ExistingImage1Path = modelOld.Image1Path,
                ExistingImage2Path = modelOld.Image2Path,
                Cities = sportObjectRepository.GetAllCities(),
                Sports = sportObjectRepository.GetAllSports()
            };

            return View(sportObjectEditViewModel);
        }


        // ***********************************
        // ************ TOURNAMENT ****************
        // ***********************************
        [HttpGet]
        public async Task<IActionResult> EditTournament(int id)
        {
            Tournament model = tournamentRepository.Get(id);
            TournamentEditViewModel tournamentEditViewModel = new TournamentEditViewModel
            {
                Id = model.Id,
                Name = model.Name,
                Email = model.Email,
                CityId = model.CityId,
                Description = model.Description,
                SportId = model.SportId,
                Phone = model.Phone,
                PriceParticipation = model.PriceParticipation,
                PricePerGame = model.PricePerGame,
                IsPayed = model.IsPayed,
                ExistingImage1Path = model.Image1Path,
                ExistingImage2Path = model.Image2Path,
                Cities = sportObjectRepository.GetAllCities(),
                Sports = sportObjectRepository.GetAllSports()
            };

            if (User.IsInRole("Client"))
            {
                var user = await userManager.FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));
                if (user == null)
                {
                    ViewBag.ErrorMessage = $"User with ID = {user.Id} cannot be found";
                    return View("NotFound");
                }

                if (user.Email != model.Email)
                {
                    return RedirectToAction("AccessDenied", "Administration");
                }
            }

            return View(tournamentEditViewModel);
        }

        [HttpPost]
        public IActionResult EditTournament(TournamentEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                Tournament tournament = tournamentRepository.Get(model.Id);
                tournament.Name = model.Name;
                tournament.Email = model.Email;
                tournament.CityId = model.CityId;
                tournament.Description = model.Description;
                tournament.SportId = model.SportId;
                tournament.Phone = model.Phone;
                tournament.PriceParticipation = model.PriceParticipation;
                tournament.PricePerGame = model.PricePerGame;
                tournament.IsPayed = model.IsPayed;

                if (model.Image1 != null)
                {
                    if (model.ExistingImage1Path != null)
                    {
                        string filePath = Path.Combine(webHostEnvironment.WebRootPath, "images", model.ExistingImage1Path);
                        System.IO.File.Delete(filePath);
                    }
                    tournament.Image1Path = ProcessUploadFile(model.Image1);
                }

                if (model.Image2 != null)
                {
                    if (model.ExistingImage2Path != null)
                    {
                        string filePath = Path.Combine(webHostEnvironment.WebRootPath, "images", model.ExistingImage2Path);
                        System.IO.File.Delete(filePath);
                    }
                    tournament.Image2Path = ProcessUploadFile(model.Image2);
                }

                tournamentRepository.Update(tournament);

                return RedirectToAction("Details", "Tournament", new { id = tournament.Id });
            }

            //return RedirectToAction("EditSportObject");

            Tournament modelOld = tournamentRepository.Get(model.Id);
            TournamentEditViewModel tournamentEditViewModel = new TournamentEditViewModel
            {
                Id = modelOld.Id,
                Name = modelOld.Name,
                Email = modelOld.Email,
                CityId = modelOld.CityId,
                Description = modelOld.Description,
                SportId = modelOld.SportId,
                Phone = modelOld.Phone,
                PriceParticipation = modelOld.PriceParticipation,
                PricePerGame = modelOld.PricePerGame,
                IsPayed = modelOld.IsPayed,
                ExistingImage1Path = modelOld.Image1Path,
                ExistingImage2Path = modelOld.Image2Path,
                Cities = sportObjectRepository.GetAllCities(),
                Sports = sportObjectRepository.GetAllSports()
            };

            return View(tournamentEditViewModel);
        }


        // ***********************************
        // ************ SPORT SCHOOL ****************
        // ***********************************
        [HttpGet]
        public async Task<IActionResult> EditSportSchool(int id)
        {
            SportSchool model = sportSchoolRepository.Get(id);
            SportSchoolEditViewModel sportSchoolEditViewModel = new SportSchoolEditViewModel
            {
                Id = model.Id,
                Name = model.Name,
                Email = model.Email,
                Address = model.Address,
                CityId = model.CityId,
                Description = model.Description,
                SportId = model.SportId,
                Phone = model.Phone,
                Price = model.Price,
                IsPayed = model.IsPayed,
                ExistingImage1Path = model.Image1Path,
                ExistingImage2Path = model.Image2Path,
                Cities = sportObjectRepository.GetAllCities(),
                Sports = sportObjectRepository.GetAllSports()
            };

            if (User.IsInRole("Client"))
            {
                var user = await userManager.FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));
                if (user == null)
                {
                    ViewBag.ErrorMessage = $"User with ID = {user.Id} cannot be found";
                    return View("NotFound");
                }

                if (user.Email != model.Email)
                {
                    return RedirectToAction("AccessDenied", "Administration");
                }
            }

            return View(sportSchoolEditViewModel);
        }

        [HttpPost]
        public IActionResult EditSportSchool(SportSchoolEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                SportSchool sportSchool = sportSchoolRepository.Get(model.Id);
                sportSchool.Name = model.Name;
                sportSchool.Email = model.Email;
                sportSchool.Address = model.Address;
                sportSchool.CityId = model.CityId;
                sportSchool.Description = model.Description;
                sportSchool.SportId = model.SportId;
                sportSchool.Phone = model.Phone;
                sportSchool.Price = model.Price;
                sportSchool.IsPayed = model.IsPayed;

                if (model.Image1 != null)
                {
                    if (model.ExistingImage1Path != null)
                    {
                        string filePath = Path.Combine(webHostEnvironment.WebRootPath, "images", model.ExistingImage1Path);
                        System.IO.File.Delete(filePath);
                    }
                    sportSchool.Image1Path = ProcessUploadFile(model.Image1);
                }

                if (model.Image2 != null)
                {
                    if (model.ExistingImage2Path != null)
                    {
                        string filePath = Path.Combine(webHostEnvironment.WebRootPath, "images", model.ExistingImage2Path);
                        System.IO.File.Delete(filePath);
                    }
                    sportSchool.Image2Path = ProcessUploadFile(model.Image2);
                }

                sportSchoolRepository.Update(sportSchool);

                return RedirectToAction("Details", "SportSchool", new { id = sportSchool.Id });
            }

            SportSchool modelOld = sportSchoolRepository.Get(model.Id);
            SportSchoolEditViewModel sportSchoolEditViewModel = new SportSchoolEditViewModel
            {
                Id = modelOld.Id,
                Name = modelOld.Name,
                Email = modelOld.Email,
                Address = modelOld.Address,
                CityId = modelOld.CityId,
                Description = modelOld.Description,
                SportId = modelOld.SportId,
                Phone = modelOld.Phone,
                Price = modelOld.Price,
                IsPayed = modelOld.IsPayed,
                ExistingImage1Path = modelOld.Image1Path,
                ExistingImage2Path = modelOld.Image2Path,
                Cities = sportObjectRepository.GetAllCities(),
                Sports = sportObjectRepository.GetAllSports()
            };

            return View(sportSchoolEditViewModel);
        }



        private string ProcessUploadFile(IFormFile image)
        {
            string uniqueFileName = null;
            if (image != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + image.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    image.CopyTo(fileStream);
                }
            }

            return uniqueFileName;
        }
    }
}