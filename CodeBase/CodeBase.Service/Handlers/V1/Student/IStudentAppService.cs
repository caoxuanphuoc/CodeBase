using System.Collections.Generic;
using System.Threading.Tasks;
using CodeBase.Service.Handlers.V1.Student.Dto;

namespace CodeBase.Service.Handlers.V1.Student
{
    public interface IStudentAppService
    {
        Task<StudentDto> CreateStudent(CreateStudentDto input);
        Task<StudentDto> GetStudentById(int id);
        Task<List<StudentDto>> GetAllStudents();
        Task<StudentDto> UpdateStudent(int id, UpdateStudentDto input);
        Task DeleteStudent(int id);
    }
}