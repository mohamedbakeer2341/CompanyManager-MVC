using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Demo.DAL.Models
{
    public class Employee : ModelBase
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = null!;
        public int? Age { get; set; }
        public string? Address { get; set; }
        public decimal Salary { get; set; }
        public bool IsActive { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime HiringDate { get; set; }
        public bool IsDeleted { get; set; }
        public string? ImageName { get; set; } = null!;
        public DateTime CreationDate { get; set; }
        public int? DepartmentId { get; set; }
        public Department? Department { get; set; } = null!;
    }
}
