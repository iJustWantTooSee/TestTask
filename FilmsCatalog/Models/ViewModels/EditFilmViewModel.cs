using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FilmsCatalog.Models.ViewModels
{
    public class EditFilmViewModel : FilmViewModel
    {
        public String PathToPoster { get; set; }
    }
}
