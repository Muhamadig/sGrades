using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace sGrades.Controllers
{
    [Authorize(Roles = "Lecturer")]
    public class CoursesController : Controller
    {
        // GET: Courses
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult New()
        {
            return View();
        }

        public ActionResult Edit(int Id)
        {
            return View("Edit",model:Id);
        }
        public ActionResult Details(int Id)
        {
            return View("Details", model: Id);
        }

    }
}