using SportskeAktivnosti.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SportskeAktivnosti.Models
{
    public class Tournament
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Morate da unesete email")]
        [RegularExpression(@"^[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?$", ErrorMessage = "Email mora da bude u formatu abc@def.com")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Morate da unesete naziv")]
        [StringLength(50, ErrorMessage = "Naziv ne moze da bude duzi od 50")]
        [Display(Name = "Naziv")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Morate da unesete cenu ucesca")]
        [Range(0, 10000, ErrorMessage = "Cena ucesca mora da bude izmedju 0 i 10000")]
        [Display(Name = "Cena ucesca")]
        public int PriceParticipation { get; set; }

        [Range(0, 10000, ErrorMessage = "Cena termina mora da bude izmedju 0 i 10000")]
        [Display(Name = "Cena po utakmici")]
        public int? PricePerGame { get; set; }

        [Required(ErrorMessage = "Morate da unesete opis")]
        [StringLength(500, ErrorMessage = "Opis ne moze da bude duzi od 500")]
        [Display(Name = "Opis")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Morate da izaberete sliku")]
        [AllowedExtensions(new string[] { ".jpg", ".png", ".jpeg" })]
        [Display(Name = "Slika 1")]
        public string Image1Path { get; set; }

        [Required(ErrorMessage = "Morate da izaberete sliku")]
        [AllowedExtensions(new string[] { ".jpg", ".png", ".jpeg" })]
        [Display(Name = "Slika 2")]
        public string Image2Path { get; set; }

        [Required(ErrorMessage = "Morate da unesete broj telefona")]
        [RegularExpression(@"^(\+381)?06(([0-6]|[8-9])(\d{7}|\d{6})){1}$", ErrorMessage = "Broj telefona mora da bude u sledecem formatu: 0641234567")]
        [Display(Name = "Broj telefona")]
        public string Phone { get; set; }


        public bool IsPayed { get; set; }



        [Required(ErrorMessage = "Morate da izaberete sport")]
        [Display(Name = "Sport")]
        public int SportId { get; set; }
        public Sport Sport { get; set; }


        [Required(ErrorMessage = "Morate da izaberete grad")]
        [Display(Name = "Grad")]
        public int CityId { get; set; }
        public City City { get; set; }
    }
}
