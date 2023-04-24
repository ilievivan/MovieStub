using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using MovieStub.Domain.DomainModels;
using MovieStub.Domain.DTO;
using MovieStub.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MovieStub.Web.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMovieService _MovieService;
        private readonly ILogger<MoviesController> _logger;

        public MoviesController(ILogger<MoviesController> logger, IMovieService MovieService)
        {
            _logger = logger;
            _MovieService = MovieService;
        }

        // GET: Movies
        public IActionResult Index()
        {
            _logger.LogInformation("User Request -> Get All Movies!");
            return View(this._MovieService.GetAllMovies());
        }

        // GET: Movies/Details/5
        public IActionResult Details(Guid? id)
        {
            _logger.LogInformation("User Request -> Get Movie Details");
            if (id == null)
            {
                return NotFound();
            }

            var Movie = this._MovieService.GetDetailsForMovie(id);
            if (Movie == null)
            {
                return NotFound();
            }

            return View(Movie);
        }

        // GET: Movies/Create
        public IActionResult Create()
        {
            _logger.LogInformation("User Request -> Get create form for Movie!");
            return View();
        }

        // POST: Movies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,MovieName,MovieImage,MovieDescription,MoviePrice,MovieRating")] Movie Movie)
        {
            _logger.LogInformation("User Request -> Insert Movie in Db!");
            if (ModelState.IsValid)
            {
                Movie.Id = Guid.NewGuid();
                this._MovieService.CreateNewMovie(Movie);
                return RedirectToAction(nameof(Index));
            }
            return View(Movie);
        }

        // GET: Movies/Edit/5
        public IActionResult Edit(Guid? id)
        {
            _logger.LogInformation("User Request -> Get edit form for Movie!");
            if (id == null)
            {
                return NotFound();
            }

            var Movie = this._MovieService.GetDetailsForMovie(id);
            if (Movie == null)
            {
                return NotFound();
            }
            return View(Movie);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("Id,MovieName,MovieImage,MovieDescription,MoviePrice,MovieRating")] Movie Movie)
        {
            _logger.LogInformation("User Request -> Update Movie in Db!");

            if (id != Movie.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    this._MovieService.UpdeteExistingMovie(Movie);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieExists(Movie.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(Movie);
        }

        // GET: Movies/Delete/5
        public IActionResult Delete(Guid? id)
        {
            _logger.LogInformation("User Request -> Get delete form for Movie!");

            if (id == null)
            {
                return NotFound();
            }

            var Movie = this._MovieService.GetDetailsForMovie(id);
            if (Movie == null)
            {
                return NotFound();
            }

            return View(Movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            _logger.LogInformation("User Request -> Delete Movie in Db!");

            this._MovieService.DeleteMovie(id);
            return RedirectToAction(nameof(Index));
        }


        public IActionResult AddMovieToCard(Guid id)
        {
            var result = this._MovieService.GetShoppingCartInfo(id);

            return View(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddMovieToCard(AddToShoppingCardDto model)
        {

            _logger.LogInformation("User Request -> Add Movie in ShoppingCart and save changes in Db!");


            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = this._MovieService.AddToShoppingCart(model, userId);

            if(result)
            {
                return RedirectToAction("Index", "Movies");
            }
            return View(model);
        }
        private bool MovieExists(Guid id)
        {
            return this._MovieService.GetDetailsForMovie(id) != null;
        }
    }
}
