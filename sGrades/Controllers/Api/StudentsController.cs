using AutoMapper;
using Microsoft.AspNet.Identity;
using sGrades.Dto;
using sGrades.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace sGrades.Controllers.Api
{
    [Authorize(Roles ="Lecturer")]
    public class StudentsController : ApiController
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


        //Get: /api/students
        public IHttpActionResult GetStudents(string query=null,int cId=0)
        {
            var currUserName = GetUserName();
            var studentsInCourse = _context.CourseEnrolls.Where(c => c.CourseId == cId).Select(c=>c.StudentId);
            var studentQuery = _context.Students.Where(s => s.LecturerId.Equals(currUserName));
            if (!String.IsNullOrWhiteSpace(query))
                studentQuery = studentQuery.Where(c => c.Id.Contains(query));

            studentQuery = studentQuery.Where(c => !studentsInCourse.Contains(c.Id));

            var studentDtos =studentQuery
                .ToList().Select(Mapper.Map<Student,StudentDto>);

            return Ok(studentDtos);
        }

        //Get /api/students/{id}

        public IHttpActionResult GetStudent(string id)
        {
            var currUserName = GetUserName();

            var student = _context.Students.Where(s => s.LecturerId.Equals(currUserName)).SingleOrDefault(s => s.Id.Equals(id));

            if (student == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<Student,StudentDto>(student));
        }

        //Post /api/students
        [HttpPost]
        public IHttpActionResult CreateStudent(StudentDto studentDto)
        {
            var currUserName = GetUserName();


            bool isExist = _context.Students.Where(s => s.LecturerId.Equals(currUserName)).Any(s => s.Id.Equals(studentDto.Id));
            if (isExist)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest) {
                    ReasonPhrase = "Student Already Exist"

                });
            }


            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

           


            var student = Mapper.Map<StudentDto,Student>(studentDto);
            student.LecturerId = currUserName;
            _context.Students.Add(student);
            _context.SaveChanges();
            return Created(new Uri(Request.RequestUri+"/"+ student.Id),studentDto);
        }


        //Put /api/students/{id}
        [HttpPut]
        public IHttpActionResult UpdateStudent(string id,StudentDto studentDto)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            }
            var currUserName = GetUserName();
            var studentInDb=_context.Students.Where(s => s.LecturerId.Equals(currUserName)).SingleOrDefault(s => s.Id.Equals(id));

            if (studentInDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);

            }
            Mapper.Map(studentDto, studentInDb);

            _context.SaveChanges();
            return Ok();
        }


        //Delete api/students/{id}
        [HttpDelete]
        public IHttpActionResult DeleteStudent(string id)
        {
            var currUserName = GetUserName();
            var studentInDb = _context.Students.Where(s => s.LecturerId.Equals(currUserName)).SingleOrDefault(s => s.Id.Equals(id));

            if (studentInDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);

            }

            _context.Students.Remove(studentInDb);
            _context.SaveChanges();
            return Ok();
        }
    }
}
