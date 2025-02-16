﻿using Company.Demo.DAL.Models;
using System.ComponentModel.DataAnnotations;

namespace Company.Demo.PL.ViewModels
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is Required !!")]
        [MaxLength(50, ErrorMessage = "Max Length Of Name Is 50 Chars")]
        [MinLength(3, ErrorMessage = "Min Length Of Name Is 3 Chars")]
        public string Name { get; set; } = null!;
        [Range(22, 60, ErrorMessage = "Age must be between (22, 60)")]
        public int? Age { get; set; }
        [RegularExpression(@"^[0-9]{1,3}-[a-zA-Z]{5,10}-[a-zA-Z]{4,10}-[a-zA-Z]{5,10}$",
            ErrorMessage = "Address must be like 123-Street-City-Country")]
        public string? Address { get; set; }
        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }
        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        [Phone]
        public string? PhoneNumber { get; set; }
        [Display(Name = "Hiring Date")]
        public DateTime HiringDate { get; set; }
        public IFormFile? Image { get; set; }
        public string? ImageName { get; set; }
        public int? DepartmentId { get; set; }
        public Department? Department { get; set; } = null!;
    }
}
