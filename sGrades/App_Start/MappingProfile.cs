using AutoMapper;
using sGrades.Dto;
using sGrades.Dtos;
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

            Mapper.CreateMap<CourseDto, Course>();
            Mapper.CreateMap<Course, CourseDto>();

            Mapper.CreateMap<CourseEnrollDto, CourseEnroll>();
            Mapper.CreateMap<CourseEnroll, CourseEnrollDto>();

            Mapper.CreateMap<AssignmentDto, Assignment>();
            Mapper.CreateMap<Assignment, AssignmentDto>();

            Mapper.CreateMap<GradeDto, Grade>();
            Mapper.CreateMap<Grade, GradeDto>();

        }

    }
}