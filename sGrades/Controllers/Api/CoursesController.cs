using AutoMapper;
using Microsoft.AspNet.Identity;
using sGrades.Dto;
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

    [Authorize(Roles = "Lecturer")]
    public class CoursesController : ApiController
    {

        private ApplicationDbContext _context;

        public CoursesController()
        {
            _context = new ApplicationDbContext();
        }

        private string GetUserName()
        {
            var currUserId = User.Identity.GetUserId();
            var currUser = _context.Users.Single(c => c.Id.Equals(currUserId));
            return currUser.UserName;
        }


        //Get: /api/courses
        public IEnumerable<CourseDto> GetCourses()
        {
            var currUserName = GetUserName();
            return _context.Courses.Where(c => c.LecturerId.Equals(currUserName)).ToList().Select(Mapper.Map<Course, CourseDto>);
        }

        //Get /api/courses/{id}

        public IHttpActionResult GetCourse(int id)
        {
            var currUserName = GetUserName();

            var course = _context.Courses.Where(c => c.LecturerId.Equals(currUserName)).SingleOrDefault(c => c.Id==id);

            if (course == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<Course, CourseDto>(course));
        }

        //Post /api/courses
        [HttpPost]
        public IHttpActionResult CreateCourse(CourseDto courseDto)
        {
            var currUserName = GetUserName();


            bool isExist = _context.Courses.Where(c => c.LecturerId.Equals(currUserName)).
                Any(c => c.Name.Equals(courseDto.Name) && c.Year==courseDto.Year && c.Semester.Equals(courseDto.Semester));
            if (isExist)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    ReasonPhrase = "This Course Already Exist"

                });
            }


            if (!ModelState.IsValid)
            {
                return BadRequest();
            }




            var course = Mapper.Map<CourseDto, Course>(courseDto);
            course.LecturerId = currUserName;
            _context.Courses.Add(course);
            _context.SaveChanges();
            return Created(new Uri(Request.RequestUri + "/" + course.Id), courseDto);
        }


        //Put /api/courses/{id}
        [HttpPut]
        public IHttpActionResult UpdateCourse(int id, CourseDto courseDto)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            }
            var currUserName = GetUserName();
            var courseInDb = _context.Courses.Where(c => c.LecturerId.Equals(currUserName)).SingleOrDefault(c => c.Id==id);

            if (courseInDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);

            }

            var originalId = courseInDb.Id;
            Mapper.Map(courseDto, courseInDb);
            courseInDb.Id = originalId;

            _context.SaveChanges();
            return Ok();
        }


        //Delete api/courses/{id}
        [HttpDelete]
        public IHttpActionResult DeleteCourse(int id)
        {
            var currUserName = GetUserName();
            var courseInDb = _context.Courses.Where(c => c.LecturerId.Equals(currUserName)).SingleOrDefault(c => c.Id==id);

            if (courseInDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);

            }

            _context.Courses.Remove(courseInDb);
            _context.SaveChanges();
            return Ok();
        }
    }
}
