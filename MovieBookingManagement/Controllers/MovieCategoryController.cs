using System.Web.Mvc;
using MovieBookingManagement.Models;

namespace MovieBookingManagement.Controllers
{
    public class MovieCategoryController : Controller
    {
        MovieCategoryDBHandle db = new MovieCategoryDBHandle();

        public ActionResult Index()
        {
            return View(db.GetCategories());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(MovieCategoryModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Cat_Type))
            {
                ModelState.AddModelError("Cat_Type", "Category name is required.");
                return View(model);
            }

            db.AddCategory(model);

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            MovieCategoryModel category = null;

            foreach (MovieCategoryModel item in db.GetCategories())
            {
                if (item.Cat_ID == id)
                {
                    category = item;
                    break;
                }
            }

            if (category == null)
                return HttpNotFound();

            return View(category);
        }

        [HttpPost]
        public ActionResult Edit(MovieCategoryModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Cat_Type))
            {
                ModelState.AddModelError("Cat_Type", "Category name is required.");
                return View(model);
            }

            db.UpdateCategory(model);

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            db.DeleteCategory(id);

            return RedirectToAction("Index");
        }
    }
}