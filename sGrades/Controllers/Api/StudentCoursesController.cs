using AutoMapper;
using Microsoft.AspNet.Identity;
using sGrades.Dtos;
using sGrades.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace sGrades.Controllers.Api
{
    [Authorize(Roles = "Student")]
    public class StudentCoursesController : ApiController
    {
        private ApplicationDbContext _context;

        public StudentCoursesController()
        {
            _context = new ApplicationDbContext();
        }

        private string GetUserName()
        {
            var currUserId = User.Identity.GetUserId();
            var currUser = _context.Users.Single(c => c.Id.Equals(currUserId));
            return currUser.UserName;
        }

        //Get: /api/StudentCourses
        public IEnumerable<CourseDto> GetCourses()
        {
            var currUserName = GetUserName();
            return _context.CourseEnrolls.Include("Course").Where(c => c.StudentId.Equals(currUserName)).Select(c => c.Course).ToList().Select(Mapper.Map<Course, CourseDto>);
            
        }

        //Get /api/StudentCourses/{id}

        public IHttpActionResult GetCourse(int id)
        {
            

            var course = _context.Courses.Single(c => c.Id == id);

            if (course == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<Course, CourseDto>(course));
        }

    }
}
