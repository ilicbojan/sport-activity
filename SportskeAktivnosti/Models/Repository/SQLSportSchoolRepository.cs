using Microsoft.EntityFrameworkCore;
using SportskeAktivnosti.Models.Database;
using SportskeAktivnosti.Models.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportskeAktivnosti.Models.Repository
{
    public class SQLSportSchoolRepository : ISportSchoolRepository
    {
        private readonly AppDbContext context;

        public SQLSportSchoolRepository(AppDbContext context)
        {
            this.context = context;
        }

        public SportSchool Add(SportSchool sportSchool)
        {
            context.SportSchools.Add(sportSchool);
            context.SaveChanges();
            return sportSchool;
        }

        public SportSchool Delete(int id)
        {
            SportSchool sportSchool = context.SportSchools.Find(id);
            if (sportSchool != null)
            {
                context.SportSchools.Remove(sportSchool);
                context.SaveChanges();
            }

            return sportSchool;
        }

        public SportSchool Get(int id)
        {
            return context.SportSchools.Include(c => c.City).Include(s => s.Sport).FirstOrDefault(s => s.Id == id);
        }

        public IEnumerable<SportSchool> GetAll()
        {
            return context.SportSchools.Include(c => c.City).Include(s => s.Sport);
        }

        public IEnumerable<City> GetAllCities()
        {
            return context.Cities;
        }

        public IEnumerable<Sport> GetAllSports()
        {
            return context.Sports;
        }

        public IEnumerable<SportSchool> GetAllWithSport(string sport)
        {
            return context.SportSchools.Include(s => s.Sport).Where(o => o.Sport.Name == sport);
        }

        public SportSchool GetByEmail(string email)
        {
            return context.SportSchools.FirstOrDefault(o => o.Email == email);
        }

        public SportSchool Update(SportSchool sportSchoolChanges)
        {
            var sportSchool = context.SportSchools.Attach(sportSchoolChanges);
            sportSchool.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();

            return sportSchoolChanges;
        }
    }
}
