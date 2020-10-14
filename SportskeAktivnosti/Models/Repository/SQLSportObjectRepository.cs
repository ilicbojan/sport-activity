using Microsoft.EntityFrameworkCore;
using SportskeAktivnosti.Models.Database;
using SportskeAktivnosti.Models.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportskeAktivnosti.Models.Repository
{
    public class SQLSportObjectRepository : ISportObjectRepository
    {
        private readonly AppDbContext context;

        public SQLSportObjectRepository(AppDbContext context)
        {
            this.context = context;
        }

        public SportObject Add(SportObject sportObject)
        {
            context.SportObjects.Add(sportObject);
            context.SaveChanges();
            return sportObject;
        }

        public SportObject Delete(int id)
        {
            SportObject sportObject = context.SportObjects.Find(id);
            if(sportObject != null)
            {
                context.SportObjects.Remove(sportObject);
                context.SaveChanges();
            }

            return sportObject;
        }

        public SportObject Get(int id)
        {
            return context.SportObjects.Include(c => c.City).Include(s => s.Sport).FirstOrDefault(s => s.Id == id);
        }

        public IEnumerable<SportObject> GetAll()
        {
            return context.SportObjects;
        }

        public IEnumerable<City> GetAllCities()
        {
            return context.Cities;
        }

        public IEnumerable<Sport> GetAllSports()
        {
            return context.Sports;
        }

        public IEnumerable<SportObject> GetAllWithSport(string sport)
        {
            return context.SportObjects.Include(s => s.Sport).Where(o => o.Sport.Name == sport);
        }

        public IEnumerable<SportObject> GetAllWithSport()
        {
            return context.SportObjects.Include(s => s.Sport);
        }

        public SportObject GetByEmail(string email)
        {
            return context.SportObjects.FirstOrDefault(o => o.Email == email);
        }

        public SportObject Update(SportObject sportObjectChanges)
        {
            var sportObject = context.SportObjects.Attach(sportObjectChanges);
            sportObject.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();

            return sportObjectChanges;
        }
    }
}
