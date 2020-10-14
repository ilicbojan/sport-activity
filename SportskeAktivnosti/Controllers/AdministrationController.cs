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
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SportskeAktivnosti.Models;
using SportskeAktivnosti.Models.Repository.Interfaces;
using SportskeAktivnosti.ViewModels;

namespace SportskeAktivnosti.Controllers {
    [Authorize (Roles = "Admin")]
    public class AdministrationController : Controller {
        private readonly ISportObjectRepository sportObjectRepository;
        private readonly ITournamentRepository tournamentRepository;
        private readonly ISportSchoolRepository sportSchoolRepository;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<IdentityUser> userManager;
        private readonly ILogger<AdministrationController> logger;

        public AdministrationController (ISportObjectRepository sportObjectRepository,
            ITournamentRepository tournamentRepository,
            ISportSchoolRepository sportSchoolRepository,
            IWebHostEnvironment webHostEnvironment,
            RoleManager<IdentityRole> roleManager,
            UserManager<IdentityUser> userManager,
            ILogger<AdministrationController> logger) {
            this.sportObjectRepository = sportObjectRepository;
            this.tournamentRepository = tournamentRepository;
            this.sportSchoolRepository = sportSchoolRepository;
            this.webHostEnvironment = webHostEnvironment;
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.logger = logger;
        }

        [HttpGet]
        public IActionResult Index () {
            return View ();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult AccessDenied () {
            return View ();
        }

        // ***********************************
        // ************ SPORT OBJECTS ****************
        // ***********************************
        [HttpGet]
        public IActionResult ListSportObjects () {
            var model = sportObjectRepository.GetAllWithSport ();

            return View (model);
        }

        [HttpGet]
        public IActionResult CreateSportObject () {
            var model = new SportObjectCreateViewModel {
                Cities = sportObjectRepository.GetAllCities (),
                Sports = sportObjectRepository.GetAllSports ()
            };

            return View (model);
        }

        [HttpPost]
        public IActionResult CreateSportObject (SportObjectCreateViewModel model) {
            if (ModelState.IsValid) {
                string uniqueFileName1 = ProcessUploadFile (model.Image1);
                string uniqueFileName2 = ProcessUploadFile (model.Image2);

                SportObject newSportObject = new SportObject {
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
                    Image1Path = uniqueFileName1,
                    Image2Path = uniqueFileName2
                };

                sportObjectRepository.Add (newSportObject);

                return RedirectToAction ("Details", "SportObject", new { id = newSportObject.Id });
            } else {
                var sportObject = new SportObjectCreateViewModel {
                    Cities = sportObjectRepository.GetAllCities (),
                    Sports = sportObjectRepository.GetAllSports ()
                };

                return View ("CreateSportObject", sportObject);
            }

        }

        [HttpPost]
        public IActionResult DeleteSportObject (int id) {
            sportObjectRepository.Delete (id);

            return RedirectToAction ("ListSportObjects");
        }

        // ***********************************
        // ************ TOURNAMENTS ****************
        // ***********************************
        [HttpGet]
        public IActionResult ListTournaments () {
            var model = tournamentRepository.GetAll ();

            return View (model);
        }

        [HttpGet]
        public IActionResult CreateTournament () {
            var model = new TournamentCreateViewModel {
                Cities = sportObjectRepository.GetAllCities (),
                Sports = sportObjectRepository.GetAllSports ()
            };

            return View (model);
        }

        [HttpPost]
        public IActionResult CreateTournament (TournamentCreateViewModel model) {
            if (ModelState.IsValid) {
                string uniqueFileName1 = ProcessUploadFile (model.Image1);
                string uniqueFileName2 = ProcessUploadFile (model.Image2);

                Tournament newTournament = new Tournament {
                    Name = model.Name,
                    Email = model.Email,
                    CityId = model.CityId,
                    Description = model.Description,
                    SportId = model.SportId,
                    Phone = model.Phone,
                    PriceParticipation = model.PriceParticipation,
                    PricePerGame = model.PricePerGame,
                    IsPayed = model.IsPayed,
                    Image1Path = uniqueFileName1,
                    Image2Path = uniqueFileName2
                };

                tournamentRepository.Add (newTournament);

                return RedirectToAction ("Details", "Tournament", new { id = newTournament.Id });
            }
            var tournament = new TournamentCreateViewModel {
                Cities = sportObjectRepository.GetAllCities (),
                Sports = sportObjectRepository.GetAllSports ()
            };

            return View ("CreateTournament", tournament);
        }

        [HttpPost]
        public IActionResult DeleteTournament (int id) {
            tournamentRepository.Delete (id);

            return RedirectToAction ("ListTournaments");
        }

        // ***********************************
        // ************ SCHOOL SPORTS ****************
        // ***********************************
        [HttpGet]
        public IActionResult ListSportSchools () {
            var model = sportSchoolRepository.GetAll ();

            return View (model);
        }

        [HttpGet]
        public IActionResult CreateSportSchool () {
            var model = new SportSchoolCreateViewModel {
                Cities = sportSchoolRepository.GetAllCities (),
                Sports = sportSchoolRepository.GetAllSports ()
            };

            return View (model);
        }

        [HttpPost]
        public IActionResult CreateSportSchool (SportSchoolCreateViewModel model) {
            if (ModelState.IsValid) {
                string uniqueFileName1 = ProcessUploadFile (model.Image1);
                string uniqueFileName2 = ProcessUploadFile (model.Image2);

                SportSchool newSportSchool = new SportSchool {
                    Name = model.Name,
                    Email = model.Email,
                    Address = model.Address,
                    CityId = model.CityId,
                    Description = model.Description,
                    SportId = model.SportId,
                    Phone = model.Phone,
                    Price = model.Price,
                    IsPayed = model.IsPayed,
                    Image1Path = uniqueFileName1,
                    Image2Path = uniqueFileName2
                };

                sportSchoolRepository.Add (newSportSchool);

                return RedirectToAction ("Details", "SportSchool", new { id = newSportSchool.Id });
            } else {
                var sportSchool = new SportSchoolCreateViewModel {
                    Cities = sportSchoolRepository.GetAllCities (),
                    Sports = sportSchoolRepository.GetAllSports ()
                };

                return View ("CreateSportSchool", sportSchool);
            }

        }

        [HttpPost]
        public IActionResult DeleteSportSchool (int id) {
            sportSchoolRepository.Delete (id);

            return RedirectToAction ("ListSportSchools");
        }

        // ***********************************
        // ************ ROLES ****************
        // ***********************************
        [HttpGet]
        public IActionResult ListRoles () {
            var roles = roleManager.Roles;

            return View (roles);
        }

        [HttpGet]
        public IActionResult CreateRole () {
            return View ();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole (CreateRoleViewModel model) {
            if (ModelState.IsValid) {
                IdentityRole identityRole = new IdentityRole {
                    Name = model.RoleName
                };

                IdentityResult result = await roleManager.CreateAsync (identityRole);

                if (result.Succeeded) {
                    return RedirectToAction ("ListRoles", "Administration");
                }

                foreach (IdentityError error in result.Errors) {
                    ModelState.AddModelError ("", error.Description);
                }
            }

            return View (model);
        }

        [HttpGet]
        public async Task<IActionResult> EditRole (string id) {
            var role = await roleManager.FindByIdAsync (id);

            if (role == null) {
                ViewBag.ErrorMessage = $"Role with Id = {id} cannot be found";
                return View ("NotFound");
            }

            var model = new EditRoleViewModel {
                Id = role.Id,
                RoleName = role.Name
            };

            foreach (var user in userManager.Users.ToList ()) {
                if (await userManager.IsInRoleAsync (user, role.Name)) {
                    model.Users.Add (user.UserName);
                }
            }

            return View (model);
        }

        [HttpPost]
        public async Task<IActionResult> EditRole (EditRoleViewModel model) {
            var role = await roleManager.FindByIdAsync (model.Id);

            if (role == null) {
                ViewBag.ErrorMessage = $"Role with Id = {model.Id} cannot be found";
                return View ("NotFound");
            } else {
                role.Name = model.RoleName;
                var result = await roleManager.UpdateAsync (role);

                if (result.Succeeded) {
                    return RedirectToAction ("ListRoles");
                }

                foreach (var error in result.Errors) {
                    ModelState.AddModelError ("", error.Description);
                }
                return View (model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> EditUserInRole (string roleId) {
            ViewBag.roleId = roleId;

            var role = await roleManager.FindByIdAsync (roleId);

            if (role == null) {
                ViewBag.ErrorMessage = $"Role with id = {roleId} cannot be found";
                return View ("NotFound");
            }

            var model = new List<RoleUsersViewModel> ();

            foreach (var user in userManager.Users.ToList ()) {
                var roleUsersViewModel = new RoleUsersViewModel {
                    UserId = user.Id,
                    UserName = user.UserName
                };

                if (await userManager.IsInRoleAsync (user, role.Name)) {
                    roleUsersViewModel.IsSelected = true;
                } else {
                    roleUsersViewModel.IsSelected = false;
                }

                model.Add (roleUsersViewModel);
            }

            return View (model);
        }

        [HttpPost]
        public async Task<IActionResult> EditUserInRole (List<RoleUsersViewModel> model, string roleId) {
            var role = await roleManager.FindByIdAsync (roleId);

            if (role == null) {
                ViewBag.ErrorMessage = $"Role with Id = {roleId} cannot be found";
                return View ("NotFound");
            }

            for (int i = 0; i < model.Count; i++) {
                var user = await userManager.FindByIdAsync (model[i].UserId);

                IdentityResult result = null;

                if (model[i].IsSelected && !(await userManager.IsInRoleAsync (user, role.Name))) {
                    result = await userManager.AddToRoleAsync (user, role.Name);
                } else if (!model[i].IsSelected && await userManager.IsInRoleAsync (user, role.Name)) {
                    result = await userManager.RemoveFromRoleAsync (user, role.Name);
                } else {
                    continue;
                }

                if (result.Succeeded) {
                    if (i < (model.Count - 1))
                        continue;
                    else
                        return RedirectToAction ("EditRole", new { Id = roleId });
                }
            }

            return RedirectToAction ("EditRole", new { Id = roleId });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteRole (string id) {
            var role = await roleManager.FindByIdAsync (id);

            if (role == null) {
                ViewBag.ErrorMessage = $"User with Id = {id} cannot be found";
                return View ("NotFound");
            } else {
                try {
                    var result = await roleManager.DeleteAsync (role);

                    if (result.Succeeded) {
                        return RedirectToAction ("ListRoles");
                    }

                    foreach (var error in result.Errors) {
                        ModelState.AddModelError ("", error.Description);
                    }

                    return View ("ListRoles");
                } catch (DbUpdateException ex) {
                    logger.LogError ($"Error deleting role {ex}");

                    ViewBag.ErrorTitle = $"{role.Name} role is in use";
                    ViewBag.ErrorMessage = $"{role.Name} role cannot be deleted as there are users in this role." +
                        $"If you want to delete this role, please remove the users from the role and then try to delete";
                    return View ("Error");
                }
            }
        }

        // ***********************************
        // ************ USERS ****************
        // ***********************************
        [HttpGet]
        public IActionResult ListUsers () {
            var users = userManager.Users;

            return View (users);
        }

        [HttpGet]
        public async Task<IActionResult> EditUser (string id) {
            var user = await userManager.FindByIdAsync (id);

            if (user == null) {
                ViewBag.ErrorMessage = $"User with ID = {id} cannot be found";
                return View ("NotFound");
            }

            var userRoles = await userManager.GetRolesAsync (user);

            var model = new EditUserViewModel {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Roles = userRoles
            };

            return View (model);
        }

        [HttpPost]
        public async Task<IActionResult> EditUser (EditUserViewModel model) {
            var user = await userManager.FindByIdAsync (model.Id);

            if (user == null) {
                ViewBag.ErrorMessage = $"User with ID = {model.Id} cannot be found";
                return View ("NotFound");
            } else {
                user.Email = model.Email;
                user.UserName = model.UserName;

                var result = await userManager.UpdateAsync (user);

                if (result.Succeeded) {
                    return RedirectToAction ("ListUsers");
                }

                foreach (var error in result.Errors) {
                    ModelState.AddModelError ("", error.Description);
                }
            }

            return View (model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser (string id) {
            var user = await userManager.FindByIdAsync (id);

            if (user == null) {
                ViewBag.ErrorMessage = $"User with Id = {id} cannot be found";
                return View ("NotFound");
            } else {
                try {
                    var result = await userManager.DeleteAsync (user);

                    if (result.Succeeded) {
                        return RedirectToAction ("ListUsers");
                    }

                    foreach (var error in result.Errors) {
                        ModelState.AddModelError ("", error.Description);
                    }

                    return View ("ListUsers");
                } catch (DbUpdateException ex) {
                    logger.LogError ($"Error deleting role {ex}");

                    ViewBag.ErrorTitle = $"{user.UserName} user have roles";
                    ViewBag.ErrorMessage = $"{user.UserName} user cannot be deleted as there are roles in this user." +
                        $"If you want to delete this user, please remove the roles from the user and then try to delete";
                    return View ("Error");
                }

            }
        }

        [HttpGet]
        public async Task<IActionResult> ManageUserRoles (string userId) {
            var user = await userManager.FindByIdAsync (userId);

            ViewBag.userId = userId;
            ViewBag.Email = user.Email;

            if (user == null) {
                ViewBag.ErrorMessage = $"User with Id = {userId} cannot be found";
                return View ("NotFound");
            }

            var model = new List<UserRolesViewModel> ();

            foreach (var role in roleManager.Roles.ToList ()) {
                var userRolesViewModel = new UserRolesViewModel {
                    RoleId = role.Id,
                    RoleName = role.Name,
                };

                if (await userManager.IsInRoleAsync (user, role.Name)) {
                    userRolesViewModel.IsSelected = true;
                } else {
                    userRolesViewModel.IsSelected = false;
                }

                model.Add (userRolesViewModel);
            }

            return View (model);
        }

        [HttpPost]
        public async Task<IActionResult> ManageUserRoles (List<UserRolesViewModel> model, string userId) {
            var user = await userManager.FindByIdAsync (userId);

            if (user == null) {
                ViewBag.ErrorMessage = $"User with Id = {userId} cannot be found";
                return View ("NotFound");
            }

            var roles = await userManager.GetRolesAsync (user);
            var result = await userManager.RemoveFromRolesAsync (user, roles);

            if (!result.Succeeded) {
                ModelState.AddModelError ("", "Cannot remove user existing roles");
                return View (model);
            }

            result = await userManager.AddToRolesAsync (user,
                model.Where (x => x.IsSelected).Select (y => y.RoleName));

            if (!result.Succeeded) {
                ModelState.AddModelError ("", "Cannot add selected roles to user");
                return View (model);
            }

            return RedirectToAction ("EditUser", new { Id = userId });
        }

        [AcceptVerbs ("Get", "Post")]
        [AllowAnonymous]
        public IActionResult IsEmailInUse (string email) {
            var sportObject = sportObjectRepository.GetByEmail (email);
            var tournament = tournamentRepository.GetByEmail (email);
            var sportSchool = sportSchoolRepository.GetByEmail (email);

            if (sportObject != null || tournament != null || sportSchool != null) {
                return Json ($"Email {email} je vec iskoriscen");
            } else {
                return Json (true);
            }
        }

        private string ProcessUploadFile (IFormFile image) {
            string uniqueFileName = null;
            if (image != null) {
                string uploadsFolder = Path.Combine (webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid ().ToString () + "_" + image.FileName;
                string filePath = Path.Combine (uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream (filePath, FileMode.Create)) {
                    image.CopyTo (fileStream);
                }
            }

            return uniqueFileName;
        }
    }
}