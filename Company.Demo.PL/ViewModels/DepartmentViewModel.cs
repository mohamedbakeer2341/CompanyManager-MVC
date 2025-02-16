using Company.Demo.DAL.Models;
using System.ComponentModel.DataAnnotations;

namespace Company.Demo.PL.ViewModels
{
    public class DepartmentViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Code is required")]
        public string Code { get; set; } = null!;
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; } = null!;
        public ICollection<Employee> Employees { get; set; } = new HashSet<Employee>();
    }
}
