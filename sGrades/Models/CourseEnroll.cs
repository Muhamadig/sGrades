using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace sGrades.Models
{
    public class CourseEnroll
    {

        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }


        [Required]
        [Index("IX_CourseEnroll", 1, IsUnique = true)]
        [ForeignKey("Course")]
        public int CourseId { get; set; }

        public Course Course { get; set; }



        [Required]
        [StringLength(9)]
        [Index("IX_CourseEnroll", 2, IsUnique = true)]
        [ForeignKey("Student"), Column(Order = 0)]
        public string StudentId { get; set; }

        [Required]
        [StringLength(9)]
        [Index("IX_CourseEnroll", 3, IsUnique = true)]
        [ForeignKey("Student"),Column(Order =1)]
        public string LecturerId { get; set; }


        public Student Student { get; set; }

        
    }
}