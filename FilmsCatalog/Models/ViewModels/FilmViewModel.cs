using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FilmsCatalog.Models.ViewModels
{
    public class FilmViewModel
    {
        [Required(ErrorMessage = "Enter the name of the film")]
        public String Name { get; set; }

        [Required(ErrorMessage = "Enter a description of the film")]
        public String Description { get; set; }

        [Required(ErrorMessage = "Enter the release year of the film")]
        public DateTime YearsOfRealease { get; set; }

        [Required(ErrorMessage = "Enter the film director")]
        public String Director { get; set; }

        public IFormFile Poster { get; set; }
    }
}
