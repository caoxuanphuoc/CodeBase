using System;
using System.ComponentModel.DataAnnotations;
using CodeBase.Core.Common;

namespace CodeBase.Core.Entities
{
    public class Student : EntityAuditBase<int>
    {
        [Required]
        [StringLength(20)]
        public string StudentCode { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }

        [Phone]
        [StringLength(20)]
        public string Phone { get; set; }

        public DateTime DateOfBirth { get; set; }

        [StringLength(200)]
        public string Address { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime? UpdatedAt { get; set; }
    }
}