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
    public class StudentGradesController : ApiController
    {
        private ApplicationDbContext _context;

        public StudentGradesController()
        {
            _context = new ApplicationDbContext();
        }

        private string GetUserName()
        {
            var currUserId = User.Identity.GetUserId();
            var currUser = _context.Users.Single(c => c.Id.Equals(currUserId));
            return currUser.UserName;
        }

        //Get: /api/StudentGrades/{id}
        public IEnumerable<GradeDto> GetCourseGrads(int id)
        {
            var currUserName = GetUserName();
            return _context.Grades.Include("Assignment")
                .Where(g => g.StudentId.Equals(currUserName)&& g.Assignment.CourseId==id).ToList().Select(Mapper.Map<Grade, GradeDto>);

        }

    }
}
