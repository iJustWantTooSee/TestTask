using FilmsCatalog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmsCatalog.Services
{
    public interface IUserPermissionsService
    {
        /// <summary>
        /// Returns information about whether the current user can edit the film
        /// </summary>
        /// <param name="film"></param>
        /// <returns>Returns true if the current user can edit the film and false if user cannot edit the film.</returns>
        Boolean IsCanEditFilm(Film film);
    }
}
