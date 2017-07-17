using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace sGrades.Models
{
    public class Course
    {
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }


        [Required]
        [Column(TypeName = "VARCHAR")]
        [StringLength(255)]
        [Index("IX_Course", 1, IsUnique = true)]
        public string Name { get; set; }


        [Required]
        [Index("IX_Course", 2, IsUnique = true)]
        public int Year { get; set; }


        [Required]
        [Column(TypeName = "VARCHAR")]
        [StringLength(10)]
        [Index("IX_Course", 3, IsUnique = true)]
        public string Semester { get; set; }


        [Required]
        [Column(TypeName = "VARCHAR")]
        [StringLength(9)]
        [Index("IX_Course", 4,IsUnique=true)]
        public string LecturerId { get; set; }
    }
}