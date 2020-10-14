using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportskeAktivnosti.Models.Repository.Interfaces
{
    public interface ISportSchoolRepository
    {
        IEnumerable<SportSchool> GetAll();
        IEnumerable<SportSchool> GetAllWithSport(string sport);
        SportSchool GetByEmail(string email);
        SportSchool Get(int id);
        SportSchool Add(SportSchool sportSchool);
        SportSchool Update(SportSchool sportSchoolChanges);
        SportSchool Delete(int id);
        IEnumerable<Sport> GetAllSports();
        IEnumerable<City> GetAllCities();
    }
}
