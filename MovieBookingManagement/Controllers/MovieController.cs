using System;
using System.Web.Mvc;
using MovieBookingManagement.Models;

namespace MovieBookingManagement.Controllers
{
    public class MovieController : Controller
    {
        MovieDBHandle db = new MovieDBHandle();

        public ActionResult Index()
        {
            var movies = db.GetMovies();
            return View(movies);
        }

        public ActionResult Create()
        {
            ViewBag.Categories = new SelectList(
                db.GetCategories(),
                "Cat_ID",
                "Cat_Type"
            );

            return View();
        }

        [HttpPost]
        public ActionResult Create(MovieModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Movie_name))
            {
                ModelState.AddModelError(
                    "Movie_name",
                    "Movie name is required."
                );
            }

            if (model.Cat_ID <= 0)
            {
                ModelState.AddModelError(
                    "Cat_ID",
                    "Please select a category."
                );
            }

            if (model.rate <= 0)
            {
                ModelState.AddModelError(
                    "rate",
                    "Rate must be greater than 0."
                );
            }

            if (ModelState.IsValid)
            {
                db.AddMovie(model);
                return RedirectToAction("Index");
            }

            ViewBag.Categories = new SelectList(
                db.GetCategories(),
                "Cat_ID",
                "Cat_Type",
                model.Cat_ID
            );

            return View(model);
        }

        public ActionResult Edit(int id)
        {
            MovieModel movie = db.GetMovieById(id);

            if (movie == null)
            {
                return HttpNotFound();
            }

            ViewBag.Categories = new SelectList(
                db.GetCategories(),
                "Cat_ID",
                "Cat_Type",
                movie.Cat_ID
            );

            return View(movie);
        }


        [HttpPost]
        public ActionResult Edit(MovieModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Movie_name))
            {
                ModelState.AddModelError(
                    "Movie_name",
                    "Movie name is required."
                );
            }

            if (model.Cat_ID <= 0)
            {
                ModelState.AddModelError(
                    "Cat_ID",
                    "Please select a category."
                );
            }

            if (model.rate <= 0)
            {
                ModelState.AddModelError(
                    "rate",
                    "Rate must be greater than 0."
                );
            }

            if (ModelState.IsValid)
            {
                db.UpdateMovie(model);
                return RedirectToAction("Index");
            }

            ViewBag.Categories = new SelectList(
                db.GetCategories(),
                "Cat_ID",
                "Cat_Type",
                model.Cat_ID
            );

            return View(model);
        }
        public ActionResult Delete(int id)
        {
            db.DeleteMovie(id);

            return RedirectToAction("Index");
        }
    }
}