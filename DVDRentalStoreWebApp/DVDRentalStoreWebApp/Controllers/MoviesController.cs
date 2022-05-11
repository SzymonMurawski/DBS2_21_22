using DVDRentalStoreWebApp.DAL;
using DVDRentalStoreWebApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DVDRentalStoreWebApp.Controllers
{
    public class MoviesController : Controller
    {
        private readonly StoreContext _context;
        public MoviesController(StoreContext context) // Dependency Injection
        {
            _context = context;
        }

        // GET: MoviesController
        public ActionResult Index(string sortOrder)
        {
            IEnumerable<Movie> movies = _context.Movies.Include(m => m.Copies);
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
            Movie movie = _context.Movies.First(m => m.Id == id);
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
                _context.Movies.Add(movie);
                _context.SaveChanges();
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
            Movie movie = _context.Movies.First(m => m.Id == id);
            return View(movie);
        }

        // POST: MoviesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [FromForm]Movie movie)
        {
            if (id != movie.Id)
            {
                return NotFound();
            }
            try
            {
                _context.Update(movie);
                _context.SaveChanges();
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
            Movie movie = _context.Movies.First(m => m.Id == id);
            return View(movie);
        }

        // POST: MoviesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            Movie movie = _context.Movies.First(m => m.Id == id);
            try
            {
                _context.Movies.Remove(movie);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(movie);
            }
        }
    }
}
