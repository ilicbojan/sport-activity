using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportskeAktivnosti.Models.Repository.Interfaces
{
    public interface ISportObjectRepository
    {
        IEnumerable<SportObject> GetAll();
        IEnumerable<SportObject> GetAllWithSport(string sport);
        IEnumerable<SportObject> GetAllWithSport();
        SportObject GetByEmail(string email);
        SportObject Get(int id);
        SportObject Add(SportObject sportObject);
        SportObject Update(SportObject sportObjectChanges);
        SportObject Delete(int id);
        IEnumerable<Sport> GetAllSports();
        IEnumerable<City> GetAllCities();
    }
}
