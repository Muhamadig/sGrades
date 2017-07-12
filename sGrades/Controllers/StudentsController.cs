using Microsoft.AspNet.Identity;
using sGrades.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace sGrades.Controllers
{
    [Authorize(Roles ="Lecturer")]
    public class StudentsController : Controller
    {

        private ApplicationDbContext _context;


        public StudentsController()
        {
            _context = new ApplicationDbContext();

            
        }

        private string GetUserName()
        {
            var currUserId = User.Identity.GetUserId();
            var currUser = _context.Users.Single(c => c.Id.Equals(currUserId));
            return currUser.UserName;
        }

        // GET: Students

        public ActionResult Index()
        {

            var currUserName = GetUserName();
            if (string.IsNullOrEmpty(currUserName))
            {
                return HttpNotFound();
            }

            var students = _context.Students.Where(u => u.LecturerId.Equals(currUserName)).ToList();

            return View(students);
        }

        public ActionResult Add()
        {
            return View();
        }
    }
}