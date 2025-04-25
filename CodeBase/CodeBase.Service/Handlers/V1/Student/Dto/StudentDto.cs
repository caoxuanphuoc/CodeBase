using CodeBase.Core.Common;
using System;
using System.ComponentModel.DataAnnotations;

namespace CodeBase.Service.Handlers.V1.Student.Dto
{
    public class StudentDto : EntityBase<int>
    {
        public string StudentCode { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        public string Phone { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string Address { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime? UpdatedAt { get; set; }
    }
}