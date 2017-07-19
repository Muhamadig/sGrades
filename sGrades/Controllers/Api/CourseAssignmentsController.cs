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
    public class CourseAssignmentsController : ApiController
    {

        private ApplicationDbContext _context;

        public CourseAssignmentsController()
        {
            _context = new ApplicationDbContext();
        }

        private string GetUserName()
        {
            var currUserId = User.Identity.GetUserId();
            var currUser = _context.Users.Single(c => c.Id.Equals(currUserId));
            return currUser.UserName;
        }

        //Get: /api/CourseAssignments/{id}
        public IEnumerable<AssignmentDto> GetCourseAssignments(int id)
        {
            var currUserName = GetUserName();
            return _context.Assignments.Where(a => a.CourseId==id).ToList().Select(Mapper.Map<Assignment, AssignmentDto>);
        }
    }
}
