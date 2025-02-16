using AutoMapper;
using Company.Demo.BLL.Interfaces;
using Company.Demo.DAL.Models;
using Company.Demo.PL.Helpers;
using Company.Demo.PL.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Drawing;

namespace Company.Demo.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public EmployeeController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index(string SearchName)
        {
            IEnumerable<Employee> employees;
            if (string.IsNullOrEmpty(SearchName))
                employees = await _unitOfWork.EmployeeRepository.GetAll();
            else
                employees = await _unitOfWork.EmployeeRepository.GetByName(SearchName);
                
            var MappedEmps = _mapper.Map<IEnumerable<EmployeeViewModel>>(employees);
            return View(MappedEmps);
        }

        [HttpGet]
        public IActionResult Create() {
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> Create(EmployeeViewModel model) {
            try
            {
                if (ModelState.IsValid)
                {
                    if (model.Image is not null)
                        model.ImageName = DocumentSettings.UploadFile(model.Image, "images");

                    var MappedEmp = _mapper.Map<Employee>(model);
                    await _unitOfWork.EmployeeRepository.Add(MappedEmp);
                    int Count = await _unitOfWork.Complete();

                    if (Count > 0)
                        TempData["Message"] = "Employee has been added successfully!";
                    else
                        TempData["Message"] = "Failed to create the employee!";
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? Id, string ViewName = "Details")
        {
            if (Id is null)
                return BadRequest();
            var employee = await _unitOfWork.EmployeeRepository.Get(Id.Value);
            if (employee is null)
                return NotFound();
            var MappedEmp = _mapper.Map<EmployeeViewModel>(employee);
            return View(ViewName, MappedEmp);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? Id)
        {
            return await Details(Id, "Edit");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Employee model) { 
            if (ModelState.IsValid)
            {
                var MappedEmp = _mapper.Map<Employee>(model);
                _unitOfWork.EmployeeRepository.Update(MappedEmp);
                int Count = await _unitOfWork.Complete();
                if (Count > 0)
                    return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? Id)
        {
            if (Id is null)
                return BadRequest();
            var employee = await _unitOfWork.EmployeeRepository.Get(Id.Value);
            if(employee is null)
                return NotFound();
            //if (Count !> 0)
            //    return NotFound();
            _unitOfWork.EmployeeRepository.Delete(employee);
            int Count = await _unitOfWork.Complete();
            if (Count > 0 && employee.ImageName is not null)
                DocumentSettings.DeleteFile(employee.ImageName, "images");
            return RedirectToAction(nameof(Index));

        }

    }
}
