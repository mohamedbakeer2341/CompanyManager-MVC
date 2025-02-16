using AutoMapper;
using Company.Demo.DAL.Models;
using Company.Demo.PL.ViewModels;

namespace Company.Demo.PL.Mapping.Departments
{
    public class DepartmentProfile : Profile
    {
        public DepartmentProfile()
        {
            CreateMap<DepartmentViewModel, Department>().ReverseMap();
        }
    }
}
