using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace sGrades.Models
{
    public class Grade
    {

        [Range(0,100)]
        public int? AssignmentGrade { get; set; }

        [Required]
        [ForeignKey("Student"), Column(Order = 0)]
        [Key]
        public string StudentId { get; set; }

        public Student Student { get; set; }

        [Required]
        [ForeignKey("Student"), Column(Order = 1)]
        [Key]
        public string LecturerId { get; set; }


        [Required]
        [ForeignKey("Assignment")]
        [Key]
        [Column(Order = 2)]
        public int AssignmentId { get; set; }

        public Assignment Assignment { get; set; }

       
    }
}