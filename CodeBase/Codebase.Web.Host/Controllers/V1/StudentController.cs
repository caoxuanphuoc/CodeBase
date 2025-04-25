using System.Collections.Generic;
using System.Threading.Tasks;
using CodeBase.Service.Handlers.V1.Student;
using CodeBase.Service.Handlers.V1.Student.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Codebase.Web.Host.Controllers.V1
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly IStudentAppService _studentAppService;

        public StudentController(IStudentAppService studentAppService)
        {
            _studentAppService = studentAppService;
        }

        /// <summary>
        /// Tạo mới sinh viên
        /// </summary>
        /// <param name="input">Thông tin sinh viên cần tạo</param>
        /// <returns>Thông tin sinh viên đã tạo</returns>
        [HttpPost]
        public async Task<ActionResult<StudentDto>> CreateStudent([FromBody] CreateStudentDto input)
        {
            var result = await _studentAppService.CreateStudent(input);
            return Ok(result);
        }

        /// <summary>
        /// Lấy danh sách tất cả sinh viên
        /// </summary>
        /// <returns>Danh sách sinh viên</returns>
        [HttpGet]
        public async Task<ActionResult<List<StudentDto>>> GetAllStudents()
        {
            var result = await _studentAppService.GetAllStudents();
            return Ok(result);
        }

        /// <summary>
        /// Lấy thông tin chi tiết sinh viên theo ID
        /// </summary>
        /// <param name="id">ID của sinh viên</param>
        /// <returns>Thông tin chi tiết sinh viên</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<StudentDto>> GetStudentById(int id)
        {
            var result = await _studentAppService.GetStudentById(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        /// <summary>
        /// Cập nhật thông tin sinh viên
        /// </summary>
        /// <param name="id">ID của sinh viên</param>
        /// <param name="input">Thông tin cần cập nhật</param>
        /// <returns>Thông tin sinh viên sau khi cập nhật</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<StudentDto>> UpdateStudent(int id, [FromBody] UpdateStudentDto input)
        {
            var result = await _studentAppService.UpdateStudent(id, input);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        /// <summary>
        /// Xóa sinh viên
        /// </summary>
        /// <param name="id">ID của sinh viên</param>
        /// <returns>Kết quả xóa</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteStudent(int id)
        {
            await _studentAppService.DeleteStudent(id);
            return Ok(new { success = true, message = "Student deleted successfully" });
        }
    }
}