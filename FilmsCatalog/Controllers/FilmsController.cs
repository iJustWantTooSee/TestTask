using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FilmsCatalog.Data;
using FilmsCatalog.Models;
using Microsoft.AspNetCore.Authorization;
using FilmsCatalog.Models.ViewModels;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
using FilmsCatalog.Services;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using FilmsCatalog.Constans;
using cloudscribe.Pagination.Models;

namespace FilmsCatalog.Controllers
{
    public class FilmsController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<User> userManager;

        private readonly IUserPermissionsService userPermissions;
        private readonly IDirectoryFilesServices directoryFiles;

        private readonly String localPathToPoster = "images/posters";
        private readonly ILogger logger;

        public FilmsController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [AllowAnonymous]
        public async Task<IActionResult> ShowCatalog(Int32 pageNumber = 1, Int32 pageSize = FilmsPaginationConstans.PageSize)
        {
            Int32 excludeRecords = (pageNumber * pageSize) - pageSize;
            var films = await this.context.Film
                .OrderByDescending(t => t.YearsOfRealease)
                .Skip(excludeRecords)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync();

            var filmsModel = new PagedResult<Film>
            {
                Data = films,
                TotalItems = this.context.Film.Count(),
                PageNumber = pageNumber,
                PageSize = pageSize

            };

            return View(filmsModel);
        }


        [Authorize]
        public IActionResult Create()
        {
            return View(new FilmViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create(FilmViewModel filmViewModel)
        {

            if (filmViewModel.Poster == null)
            {
                this.ModelState.AddModelError(nameof(filmViewModel.Poster), "Upload an image for the poster");
                return View(filmViewModel);
            }

            if (!directoryFiles.IsAllowedFileFormat(filmViewModel.Poster))
            {
                this.ModelState.AddModelError(nameof(filmViewModel.Poster), "This file type is prohibited");
            }

            var user = await this.userManager.GetUserAsync(this.HttpContext.User);
            if (ModelState.IsValid && user != null)
            {
                var film = new Film
                {
                    Id = Guid.NewGuid(),
                    Name = filmViewModel.Name,
                    Description = filmViewModel.Description,
                    YearsOfRealease = filmViewModel.YearsOfRealease,
                    Director = filmViewModel.Director,
                    CreatorId = user.Id

                };
                film.PathToPoster = this.directoryFiles
                    .GetNewLocalFilePath(filmViewModel.Poster, this.localPathToPoster);

                await this.directoryFiles.AddFileToServer(filmViewModel.Poster, film.PathToPoster);

                try
                {
                    this.logger.LogInformation($"Created a film with: \n" +
                        $" Id: {film.Id} \n" +
                        $" Added a film poster. The local path: {film.PathToPoster}\n " +
                        $"The user who created the description: {user}\n"
                       );
                    this.context.Add(film);
                    await this.context.SaveChangesAsync();
                }
                catch
                {
                    throw;
                }
                return RedirectToAction(nameof(ShowCatalog));
            }

            return View(filmViewModel);
        }


    }
}
