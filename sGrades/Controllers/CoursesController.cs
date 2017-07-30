using sGrades.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace sGrades.Controllers
{
    [RoleAuthorization(Roles = "Lecturer")]

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

        public ActionResult Enroll(int Id)
        {
            return View("Enroll", model: Id);
        }

        public ActionResult NewAssignment(int Id)
        {
            return View("NewAssignment", model: Id);
        }

        public ActionResult EditAssignment(int Id)
        {
            return View("EditAssignment", model: Id);
        }

        public ActionResult Grades(int Id)
        {
            return View("Grades", model: Id);
        }
    }
}