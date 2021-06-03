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
        [Required(ErrorMessage = "Введите название фильма")]
        public String Name { get; set; }

        [Required(ErrorMessage = "Введите описание к фильму")]
        public String Description { get; set; }

        [Required(ErrorMessage = "Введите год выхода фильма")]
        public DateTime YearsOfRealease { get; set; }

        [Required(ErrorMessage = "Введите режиссера фильма")]
        public String Director { get; set; }

        public IFormFile Poster { get; set; }
    }
}
