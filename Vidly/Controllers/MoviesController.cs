using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using Vidly.Models;
using Vidly.ViewModels;
using System.Data.Entity;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        readonly Movies _movies = new Movies
        {
            MovieList = new List<Movie>
            {
                new Movie { Id = 1, Name = "Shrek"},
                new Movie { Id = 1, Name = "Wall-e"}
            }
        };

        private ApplicationDbContext _context;

        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Movies
        public ActionResult Random()
        {
            var movie = new Movie {Name = "Shrek!"};
            var customers = new List<Customer>
            {
                new Customer { Name = "Customer 1"},
                new Customer { Name = "Customer 2"},
                new Customer { Name = "Customer 3"},
                new Customer { Name = "Customer 4"},
                new Customer { Name = "Customer 5"},
                new Customer { Name = "Customer 6"}
            };
            var viewModel = new RandomMovieViewModel {Customers = customers, Movie = movie};
            return View(viewModel);
            
        }

        public ActionResult Edit(int id)
        {
            return Content("id =" + id);
        }

        [Route("movies/released/{year}/{month:regex(\\d{2}):range(1, 12)}")]
        public ActionResult ByReleaseDate(int year, byte month)
        {
            return Content(year + "/" + month);
        }

        public ActionResult Index()
        {
            var movies = _context.Movies.Include(c => c.Genre).ToList();
            return View(movies);
        }

        [Route("Movies/Details/{id}")]
        public ActionResult Details(int id)
        {
            var movie = _context.Movies.Include(c => c.Genre).ToList().SingleOrDefault(i => i.Id == id);
            return movie == null ? (ActionResult)HttpNotFound() : View(movie);
        }
    }
}