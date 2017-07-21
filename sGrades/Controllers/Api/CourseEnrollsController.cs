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
    [Authorize(Roles = "Lecturer")]
    public class CourseEnrollsController : ApiController
    {

        private ApplicationDbContext _context;

        public CourseEnrollsController()
        {
            _context = new ApplicationDbContext();
        }

        private string GetUserName()
        {
            var currUserId = User.Identity.GetUserId();
            var currUser = _context.Users.Single(c => c.Id.Equals(currUserId));
            return currUser.UserName;
        }


        //Get: /api/CourseEnrolls
        public IEnumerable<CourseEnrollDto> GetCoursesEnrolls()
        {
            var currUserName = GetUserName();
            return _context.CourseEnrolls.Where(c => c.LecturerId.Equals(currUserName)).ToList().Select(Mapper.Map<CourseEnroll, CourseEnrollDto>);
        }

        //Get /api/CourseEnrolls/{id}

        public IEnumerable<CourseEnrollDto> GetCourseEnrolls(int id)
        {
            var currUserName = GetUserName();

            var course = _context.Courses.Where(c => c.LecturerId.Equals(currUserName)).SingleOrDefault(c => c.Id == id);

            if (course == null)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    ReasonPhrase = "The course not found"

                });
            }

            return _context.CourseEnrolls.Include("Student").Where(c => c.LecturerId.Equals(currUserName) && c.CourseId==course.Id).ToList().Select(Mapper.Map<CourseEnroll, CourseEnrollDto>);
        }

        //Post /api/CourseEnrolls
        [HttpPost]
        public IHttpActionResult CreateCourseEnroll(CourseEnrollDto courseEnrollDto)
        {
            var currUserName = GetUserName();


            bool isExist = _context.CourseEnrolls.Where(c => c.LecturerId.Equals(currUserName)).
                Any(c => c.StudentId.Equals(courseEnrollDto.StudentId) && c.CourseId == courseEnrollDto.CourseId);
            if (isExist)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    ReasonPhrase = "This Student Already Enrolled To This Course"

                });
            }

            isExist = _context.Students.Where(c => c.LecturerId.Equals(currUserName)).Any(s => s.Id.Equals(courseEnrollDto.StudentId));
            if (!isExist)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    ReasonPhrase = "This Student is not exist in your students list, please add him first to my students"

                });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }




            var courseEnroll = Mapper.Map<CourseEnrollDto, CourseEnroll>(courseEnrollDto);
            courseEnroll.LecturerId = currUserName;
            courseEnrollDto.LecturerId = currUserName;
            _context.CourseEnrolls.Add(courseEnroll);
            _context.SaveChanges();

            //check if there is any assignments for the course thin we have to create grades for the student

            List<Assignment> courseAssignments=_context.Assignments.Where(a => a.CourseId == courseEnroll.CourseId).ToList();
            if(courseAssignments!=null && courseAssignments.Any())
            {
                foreach(var assignment in courseAssignments)
                {
                    var grade = new Grade
                    {
                        AssignmentGrade = null,
                        Assignment = assignment,
                        AssignmentId = assignment.Id,
                        LecturerId = currUserName,
                        Student = _context.Students.Single(s=>s.LecturerId.Equals(currUserName) && s.Id.Equals(courseEnroll.StudentId)),
                        StudentId = courseEnroll.StudentId
                    };

                    _context.Grades.Add(grade);
                }
            }
            _context.SaveChanges();

            return Created(new Uri(Request.RequestUri + "/" + courseEnroll.CourseId), courseEnrollDto);
        }


        //Delete api/CourseEnrolls/{id}
        [HttpDelete]
        public IHttpActionResult DeleteCourseEnroll(int id)
        {
            var currUserName = GetUserName();
            var enrollInDb = _context.CourseEnrolls.Where(c => c.LecturerId.Equals(currUserName)).SingleOrDefault(c => c.Id==id);

            if (enrollInDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);

            }


            List<Assignment> courseAssignments = _context.Assignments.Where(a => a.CourseId == enrollInDb.CourseId).ToList();
            if (courseAssignments != null && courseAssignments.Any())
            {
                foreach (var assignment in courseAssignments)
                {
                    _context.Grades.Remove(_context.Grades
                        .Single(g => g.AssignmentId == assignment.Id 
                        && g.LecturerId.Equals(enrollInDb.LecturerId) 
                        && g.StudentId.Equals(enrollInDb.StudentId)));
                }
            }
            _context.SaveChanges();
            _context.CourseEnrolls.Remove(enrollInDb);
            _context.SaveChanges();
            return Ok();
        }

    }
}
