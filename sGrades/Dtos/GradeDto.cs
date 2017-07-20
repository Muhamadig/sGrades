using sGrades.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace sGrades.Dtos
{
    public class GradeDto
    {

        [Range(0, 100)]
        public int? AssignmentGrade { get; set; }

        [Required]
        
        public string StudentId { get; set; }

        public Student Student { get; set; }

        
        
        public string LecturerId { get; set; }


        [Required]
        
        public int AssignmentId { get; set; }

        public Assignment Assignment { get; set; }
    }
}
