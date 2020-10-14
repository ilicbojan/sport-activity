using Microsoft.AspNetCore.Http;
using SportskeAktivnosti.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SportskeAktivnosti.ViewModels
{
    public class SportSchoolEditViewModel : SportSchoolCreateViewModel
    {
        public int Id { get; set; }
        public string ExistingImage1Path { get; set; }
        public string ExistingImage2Path { get; set; }

        [AllowedExtensions(new string[] { ".jpg", ".png", ".jpeg" })]
        [Display(Name = "Slika 1")]
        public new IFormFile Image1 { get; set; }


        [AllowedExtensions(new string[] { ".jpg", ".png", ".jpeg" }, ErrorMessage = "Slika mora da bude u jpg, jpeg ili png formatu")]
        [Display(Name = "Slika 2")]
        public new IFormFile Image2 { get; set; }

        [Required(ErrorMessage = "Morate da unesete email")]
        [RegularExpression(@"^[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?$", ErrorMessage = "Email mora da bude u formatu abc@def.com")]
        public new string Email { get; set; }
    }
}
