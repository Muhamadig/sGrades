using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace sGrades.Dto
{
    public class StudentDto
    {

        [Required]
        [MaxLength(9, ErrorMessage = "The student id is 9 digits")]
        [MinLength(9, ErrorMessage = "The student id is 9 digits")]
        [Key]
        [Column(Order = 0)]
        public string Id { get; set; }

        //[MaxLength(9)]
        //[MinLength(9)]
        //[Key]
        //[Column(Order = 1)]
        //public string LecturerId { get; set; }

        [Required]
        public string FName { get; set; }

        [Required]
        public string LName { get; set; }
    }
}