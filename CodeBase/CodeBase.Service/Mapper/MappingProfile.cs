using AutoMapper;
using CodeBase.Core.Entities;
using CodeBase.Service.Handlers.V1.Example.Dto;
using CodeBase.Service.Handlers.V1.Student.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeBase.Service.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Define your mappings here
            // CreateMap<Source, Destination>();
            #region Example
            CreateMap<ExampleEntity, ExampleDto>();
            CreateMap<ExampleDto, ExampleEntity>();
            CreateMap<CreateExampleDto, ExampleEntity>();
            CreateMap<UpdateExampleDto, ExampleEntity>();
            CreateMap< ExampleEntity, UpdateExampleDto>();
            #endregion

            #region Example
            CreateMap<Student, StudentDto>();
            CreateMap<StudentDto, Student>();
            CreateMap<CreateStudentDto, Student>();
            CreateMap<UpdateStudentDto, Student>();
            CreateMap<Student, UpdateStudentDto>();
            #endregion
        }
    }
}
