using sGrades.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace sGrades.Controllers
{
    [RoleAuthorization(Roles = "Student")]
    public class StudentCoursesController : Controller
    {
        // GET: StudentCourses
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Grades(int id)
        {
            return View("Grades",model:id);
        }
    }
}