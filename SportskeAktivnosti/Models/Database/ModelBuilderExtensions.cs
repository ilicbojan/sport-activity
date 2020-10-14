using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportskeAktivnosti.Models.Database
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Country>().HasData(
                new Country
                {
                    Id = 1,
                    Name = "Srbija"
                },
                new Country
                {
                    Id = 2,
                    Name = "Hrvatska"
                },
                new Country
                {
                    Id = 3,
                    Name = "BiH"
                },
                new Country
                {
                    Id = 4,
                    Name = "Crna Gora"
                }
            );

            modelBuilder.Entity<City>().HasData(
                new City
                {
                    Id = 1,
                    Name = "Beograd",
                    CountryId = 1
                },
                new City
                {
                    Id = 2,
                    Name = "Novi Sad",
                    CountryId = 1
                },
                new City
                {
                    Id = 3,
                    Name = "Zagreb",
                    CountryId = 2
                },
                new City
                {
                    Id = 4,
                    Name = "Sarajevo",
                    CountryId = 3
                },
                new City
                {
                    Id = 5,
                    Name = "Podgorica",
                    CountryId = 4
                }
            );

            modelBuilder.Entity<Sport>().HasData(
                new Sport
                {
                    Id = 1,
                    Name = "Fudbal"
                },
                new Sport
                {
                    Id = 2,
                    Name = "Kosarka"
                },
                new Sport
                {
                    Id = 3,
                    Name = "Tenis"
                },
                new Sport
                {
                    Id = 4,
                    Name = "Teretana"
                },
                new Sport
                {
                    Id = 5,
                    Name = "Bazen"
                }
            );

            
        }
    }
}
