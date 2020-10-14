using Microsoft.EntityFrameworkCore;
using SportskeAktivnosti.Models.Database;
using SportskeAktivnosti.Models.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportskeAktivnosti.Models.Repository
{
    public class SQLTournamentRepository : ITournamentRepository
    {
        private readonly AppDbContext context;

        public SQLTournamentRepository(AppDbContext context)
        {
            this.context = context;
        }

        public Tournament Add(Tournament tournament)
        {
            context.Tournaments.Add(tournament);
            context.SaveChanges();
            return tournament;
        }

        public Tournament Delete(int id)
        {
            Tournament tournament = context.Tournaments.Find(id);
            if (tournament != null)
            {
                context.Tournaments.Remove(tournament);
                context.SaveChanges();
            }

            return tournament;
        }

        public Tournament Get(int id)
        {
            return context.Tournaments.Include(c => c.City).Include(s => s.Sport).FirstOrDefault(s => s.Id == id);
        }

        public IEnumerable<Tournament> GetAll()
        {
            return context.Tournaments.Include(c => c.City).Include(s => s.Sport);
        }

        public IEnumerable<City> GetAllCities()
        {
            return context.Cities;
        }

        public IEnumerable<Sport> GetAllSports()
        {
            return context.Sports;
        }

        public IEnumerable<Tournament> GetAllWithSport(string sport)
        {
            return context.Tournaments.Include(s => s.Sport).Where(o => o.Sport.Name == sport);
        }

        public Tournament GetByEmail(string email)
        {
            return context.Tournaments.FirstOrDefault(o => o.Email == email);
        }

        public Tournament Update(Tournament tournamentChanges)
        {
            var tournament = context.Tournaments.Attach(tournamentChanges);
            tournament.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();

            return tournamentChanges;
        }
    }
}
