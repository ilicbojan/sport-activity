using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportskeAktivnosti.Models.Repository.Interfaces
{
    public interface ITournamentRepository
    {
        IEnumerable<Tournament> GetAll();
        IEnumerable<Tournament> GetAllWithSport(string sport);
        Tournament GetByEmail(string email);
        Tournament Get(int id);
        Tournament Add(Tournament tournament);
        Tournament Update(Tournament tournamentChanges);
        Tournament Delete(int id);
        IEnumerable<Sport> GetAllSports();
        IEnumerable<City> GetAllCities();
    }
}
