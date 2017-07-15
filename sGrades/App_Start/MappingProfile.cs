using AutoMapper;
using sGrades.Dto;
using sGrades.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sGrades.App_Start
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            Mapper.CreateMap<Student, StudentDto>();
            Mapper.CreateMap<StudentDto, Student>();

        }

    }
}