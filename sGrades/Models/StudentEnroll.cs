using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace sGrades.Models
{
    public class StudentEnroll
    {
        [Required]
        public int Id { get; set; }

        
        public string StudentId { get; set; }

        public Student Student { get; set; }

        [Required]
        public string LecturerId { get; set; }
    }
}