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
    public class AssignmentsController : ApiController
    {
        private ApplicationDbContext _context;

        public AssignmentsController()
        {
            _context = new ApplicationDbContext();
        }

        private string GetUserName()
        {
            var currUserId = User.Identity.GetUserId();
            var currUser = _context.Users.Single(c => c.Id.Equals(currUserId));
            return currUser.UserName;
        }

        //Get /api/assignments/{id}

        public IHttpActionResult GetAssignment(int id)
        {
            var currUserName = GetUserName();

            var assignment = _context.Assignments.SingleOrDefault(a => a.Id==id);

            if (assignment == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<Assignment, AssignmentDto>(assignment));
        }

        //Post /api/assignments
        [HttpPost]
        public IHttpActionResult CreateAssignment(AssignmentDto assignmentDto)
        {
            var currUserName = GetUserName();


            bool isExist = _context.Assignments.Where(a => a.CourseId==assignmentDto.CourseId).
                Any(a => a.Name.Equals(assignmentDto.Name) && a.Type.Equals(assignmentDto.Type));
            if (isExist)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    ReasonPhrase = "This Assignment Is Exist ,Please Choose Another Name Or Type."

                });
            }


            if (!ModelState.IsValid)
            {
                return BadRequest();
            }




            var assignment = Mapper.Map<AssignmentDto, Assignment>(assignmentDto);
            _context.Assignments.Add(assignment);
            _context.SaveChanges();

            List<Student> courseStudents = _context.CourseEnrolls.Where(c=>c.CourseId==assignment.CourseId).Select(c=>c.Student).ToList();
            foreach(var student in courseStudents)
            {
                var grade = new Grade {
                    AssignmentGrade = null,
                    Assignment=assignment,
                    AssignmentId=assignment.Id,
                    LecturerId=currUserName,
                    Student=student,
                    StudentId=student.Id
                };

                _context.Grades.Add(grade);
            }
            _context.SaveChanges();

            return Created(new Uri(Request.RequestUri + "/" + assignment.CourseId), assignmentDto);
        }

        //Put /api/assignments/{id}
        [HttpPut]
        public IHttpActionResult UpdateAssignment(int id, AssignmentDto assignmentDto)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            }
            var currUserName = GetUserName();
            var assignmentInDb = _context.Assignments.SingleOrDefault(a => a.Id == id);

            if (assignmentInDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);

            }
            bool isExist = _context.Assignments
                 .Any(a => a.Name.Equals(assignmentDto.Name) && a.Type.Equals(assignmentDto.Type));

            if (isExist)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    ReasonPhrase = "Can't update,this Assignment Already Exist"

                });
            }
            var originalId = assignmentInDb.Id;
            Mapper.Map(assignmentDto, assignmentInDb);
            assignmentInDb.Id = originalId;

            _context.SaveChanges();
            return Ok();
        }
        //Delete api/assignments/{id}
        [HttpDelete]
        public IHttpActionResult DeleteAssignment(int id)
        {
            var currUserName = GetUserName();
            var assignmentsInDb = _context.Assignments.SingleOrDefault(a => a.Id==id);

            if (assignmentsInDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);

            }


            List<Grade> assignmentGrades=_context.Grades.Where(g => g.AssignmentId == assignmentsInDb.Id).ToList();

            foreach(var grade in assignmentGrades)
            {
                _context.Grades.Remove(grade);
            }

            _context.Assignments.Remove(assignmentsInDb);
            _context.SaveChanges();

            
            return Ok();
        }

    }
}
