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
    public class GradesController : ApiController
    {
        private ApplicationDbContext _context;

        public GradesController()
        {
            _context = new ApplicationDbContext();
        }

        private string GetUserName()
        {
            var currUserId = User.Identity.GetUserId();
            var currUser = _context.Users.Single(c => c.Id.Equals(currUserId));
            return currUser.UserName;
        }


        //Get: /api/Grades/{id}
        public IEnumerable<GradeDto> GetGrades(int id)
        {
            var currUserName = GetUserName();
            return _context.Grades.Where(g => g.AssignmentId==id).ToList().Select(Mapper.Map<Grade, GradeDto>);
        }


        //Put /api/Grades
        [HttpPut]
        public IHttpActionResult UpdateGrade(GradesDto gradesDto)
        {

            foreach (var grade in gradesDto.Grades)
            {
                if (grade.AssignmentGrade != null && (grade.AssignmentGrade.Value < 0 || grade.AssignmentGrade.Value > 100)) { 
                    throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest)
                    {
                        ReasonPhrase = "The grade of student: "+grade.StudentId+" Is Invalid- the grade value should be at range (0,100) or empty"

                    });
                }
            }
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            }
            var currUserName = GetUserName();

            foreach (var grade in gradesDto.Grades)
            {
                var gradeInDb = _context.Grades.Single(g => g.LecturerId.Equals(currUserName) && g.AssignmentId == grade.AssignmentId && g.StudentId.Equals(grade.StudentId));
                if (gradeInDb == null)
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);

                }

                Mapper.Map(grade, gradeInDb);
                gradeInDb.LecturerId = currUserName;
                _context.SaveChanges();
            }


            
            return Ok();
        }

    }
}
