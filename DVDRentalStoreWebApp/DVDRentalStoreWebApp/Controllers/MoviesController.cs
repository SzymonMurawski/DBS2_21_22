using DVDRentalStoreWebApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DVDRentalStoreWebApp.Controllers
{
    public class MoviesController : Controller
    {
        private static List<Movie> Movies = new List<Movie>
        {
            new Movie(1, "Anchorman", 2000, 12, new List<Copy> { new Copy(1, true, 1), new Copy(2, true, 1), new Copy(3, false, 1) }),
            new Movie(2, "Anchorman 2", 2001, 7),
            new Movie(3, "Terminator", 1993, 22, new List<Copy> { new Copy(1, true, 1), new Copy(2, true, 1) }),
            new Movie(4, "Jurrasic Park", 1999, 29),
            new Movie(5, "The Lord of the Rings", 1997, 82, new List<Copy> { new Copy(1, true, 1) }),
        };
        // GET: MoviesController
        public ActionResult Index(string sortOrder)
        {
            IEnumerable<Movie> movies = Movies;
            ViewBag.NextSortOrder = sortOrder == null || sortOrder == "descending" ? "ascending" : "descending";
            switch (sortOrder)
            {
                case "descending":
                    movies = movies.OrderByDescending(m => m.Title);
                    break;
                case "ascending":
                    movies = movies.OrderBy(m => m.Title);
                    break;
                default:
                    break;
            }
            return View(movies);
        }

        // GET: MoviesController/Details/5
        public ActionResult Details(int id)
        {
            Movie movie = Movies.Find(m => m.Id == id);
            return View(movie);
        }

        // GET: MoviesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MoviesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([FromForm]Movie movie)
        {
            try
            {
                Movies.Add(movie);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MoviesController/Edit/5
        public ActionResult Edit(int id)
        {
            Movie movie = Movies.Find(m => m.Id == id);
            return View(movie);
        }

        // POST: MoviesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            Movie movie = Movies.Find(m => m.Id == id);
            try
            {
                movie.Title = collection["Title"];
                movie.Year = int.Parse(collection["Year"]);
                movie.Price = double.Parse(collection["Price"]);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(movie);
            }
        }

        // GET: MoviesController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: MoviesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
