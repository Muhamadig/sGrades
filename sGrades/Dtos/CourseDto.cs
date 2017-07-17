using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace sGrades.Dtos
{
    public class CourseDto
    {


        public int Id { get; set; }

        public string Name { get; set; }


        
        public int Year { get; set; }


        
        public string Semester { get; set; }
    }
}