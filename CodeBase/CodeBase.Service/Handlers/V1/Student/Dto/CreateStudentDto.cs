using System;

namespace CodeBase.Service.Handlers.V1.Student.Dto
{
    public class CreateStudentDto
    {
        public string StudentCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
    }
}