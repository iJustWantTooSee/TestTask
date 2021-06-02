using FilmsCatalog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmsCatalog.Services
{
    public interface IUserPermissionsService
    {
        Boolean IsCanEditFilm(Film film);
    }
}
