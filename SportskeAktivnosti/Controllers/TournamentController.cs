using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportskeAktivnosti.Models;
using SportskeAktivnosti.Models.Repository.Interfaces;

namespace SportskeAktivnosti.Controllers
{
    [AllowAnonymous]

    public class TournamentController : Controller
    {
        private readonly ITournamentRepository tournamentRepository;

        public TournamentController(ITournamentRepository tournamentRepository)
        {
            this.tournamentRepository = tournamentRepository;
        }

        public IActionResult List()
        {
            var model = tournamentRepository.GetAll();

            return View(model);
        }

        public IActionResult Details(int id)
        {
            var model = tournamentRepository.Get(id);

            return View(model);
        }
    }
}