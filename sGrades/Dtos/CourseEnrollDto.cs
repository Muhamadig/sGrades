using sGrades.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace sGrades.Dtos
{
    public class CourseEnrollDto
    {
        public int Id { get; set; }

        public int CourseId { get; set; }

       
        public string StudentId { get; set; }

        
        public string LecturerId { get; set; }

        public StudentDto Student { get; set; }
    }
}