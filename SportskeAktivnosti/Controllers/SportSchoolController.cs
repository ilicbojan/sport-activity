using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportskeAktivnosti.Models.Repository.Interfaces;

namespace SportskeAktivnosti.Controllers
{
    [AllowAnonymous]

    public class SportSchoolController : Controller
    {
        private readonly ISportSchoolRepository sportSchoolRepository;

        public SportSchoolController(ISportSchoolRepository sportSchoolRepository)
        {
            this.sportSchoolRepository = sportSchoolRepository;
        }

        public IActionResult List()
        {
            var model = sportSchoolRepository.GetAll();

            return View(model);
        }

        public IActionResult Details(int id)
        {
            var model = sportSchoolRepository.Get(id);

            return View(model);
        }
    }
}