using AutoMapper;
using Company.Demo.BLL.Interfaces;
using Company.Demo.BLL.Repositories;
using Company.Demo.DAL.Models;
using Company.Demo.PL.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Company.Demo.PL.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public DepartmentController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var departments = await _unitOfWork.DepartmentRepository.GetAll();
            var mappedDepartments = _mapper.Map<IEnumerable<DepartmentViewModel>>(departments);
            return View(mappedDepartments);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(DepartmentViewModel model)
        {
            if (ModelState.IsValid)
            {
                var mappedModel = _mapper.Map<Department>(model);
                await _unitOfWork.DepartmentRepository.Add(mappedModel);
                int Count = await _unitOfWork.Complete();
                if (Count > 0)
                    TempData["Message"] = "Department has been created successfully !";
                else
                    TempData["Message"] = "Failed to create the department !";
                return RedirectToAction(nameof(Index));

            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? Id, string ViewName = "Details")
        {
            if (Id is null)
                return BadRequest();
            var department = await _unitOfWork.DepartmentRepository.Get(Id.Value);
            if (department == null)
                return NotFound();
            var MappedDepartment = _mapper.Map<DepartmentViewModel>(department);
            return View(ViewName, MappedDepartment);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? Id)
        {
            return await Details(Id, "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(DepartmentViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var mappedModel = _mapper.Map<Department>(model);
                    _unitOfWork.DepartmentRepository.Update(mappedModel);
                    int Count = await _unitOfWork.Complete();
                    if (Count > 0)
                        return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex) {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return View(model);
        }

        //[HttpGet]
        //public IActionResult Delete(int? Id)
        //{
        //    return Details(Id, "Delete");
        //}
        [HttpPost]
        public async Task<IActionResult> Delete(int Id)
        {
            var department = await _unitOfWork.DepartmentRepository.Get(Id);
            if(department is null)
                return NotFound();
            _unitOfWork.DepartmentRepository.Delete(department);
            await _unitOfWork.Complete();
            return RedirectToAction(nameof(Index));
        }
    }
}
