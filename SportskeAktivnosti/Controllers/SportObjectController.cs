using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportskeAktivnosti.Models;
using SportskeAktivnosti.Models.Database;
using SportskeAktivnosti.Models.Repository.Interfaces;
using SportskeAktivnosti.ViewModels;

namespace SportskeAktivnosti.Controllers
{
    [AllowAnonymous]
    public class SportObjectController : Controller
    {
        private readonly ISportObjectRepository sportObjectRepository;

        public SportObjectController(ISportObjectRepository sportObjectRepository)
        {
            this.sportObjectRepository = sportObjectRepository;
        }

        public IActionResult List(string sport)
        {
            ViewBag.Sport = sport;
            var model = sportObjectRepository.GetAllWithSport(sport);

            return View(model);
        }

        public IActionResult Details(int id)
        {
            var model = sportObjectRepository.Get(id);

            return View(model);
        }

        
    }
}