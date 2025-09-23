using Demo.BusnissLogic.DataTransferObjects;
using Demo.BusnissLogic.DataTransferObjects.Departments;
using Demo.BusnissLogic.Services.Interfaces;
using Demo.PresentationLayer.ViewModels.Departments;
using Microsoft.AspNetCore.Mvc;

namespace Demo.PresentationLayer.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentServices _departmentServices;
        private readonly ILogger<DepartmentController> _logger;
        private readonly IWebHostEnvironment _env;

        public DepartmentController(IDepartmentServices departmentServices,
            ILogger<DepartmentController> logger,
            IWebHostEnvironment env)
        {
            _departmentServices = departmentServices;
            _logger = logger;
            _env = env;
        }
        [HttpGet]
        public IActionResult Index()
        {
            //ViewData["Message"] = new DepartmentDto() {Name = "Hello From ViewData" };
            //ViewBag.Message = new DepartmentDto() { Name = "Hello From ViewBag" };
            var departments = _departmentServices.GetAllDepartments();
            return View(departments);
        }

        #region Create
        // Show the form to create a new department
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(DepartmentViewModel departmentVM)
        {
            // Server-side validation
            if (!ModelState.IsValid)
            {
                return View(departmentVM);
            }
            var massage = "";
            try
            {
                var departmentDto = new CreatedDepartmentDto()
                {
                    Name = departmentVM.Name,
                    Code = departmentVM.Code,
                    Description = departmentVM.Description,
                    DateofCreation = departmentVM.DateofCreation
                };
                var result = _departmentServices.AddDepartment(departmentDto);
                if (result > 0)
                {
                   massage = "Department Created Successfully";
                }
                else
                {
                    massage = "Department Can't be Created now , try again later :(";
                }
                TempData["Message"] = massage;
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Log the exception
                _logger.LogError(ex, massage);
                if (_env.IsDevelopment())
                {
                    massage = ex.Message;
                    return View(departmentVM);
                }
                else
                {
                    massage = "Department Can't be Created";
                    return View("Error", massage);
                }
            }
        }
        #endregion

        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id is null || id <= 0)
            {
                return BadRequest("Invalid department ID."); // 400 Bad Request 
            }
            var department = _departmentServices.GetDepartmentById(id.Value);
            if (department is null)
            {
                return NotFound("Department not found."); // 404 Not Found
            }
            return View(department);

        }

        #region Edit
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id is null || id <= 0)
            {
                return BadRequest("Invalid department ID."); // 400 Bad Request 
            }
            var department = _departmentServices.GetDepartmentById(id.Value);
            if (department is null)
            {
                return NotFound("Department not found."); // 404 Not Found
            }
            return View(new DepartmentViewModel()
            {
                Name = department.Name,
                Code = department.Code,
                Description = department.Description,
                DateofCreation = department.DateofCreation
            });
        }

        [HttpPost]
        public IActionResult Edit([FromRoute] int id, DepartmentViewModel departmentVM)
        {
            if (!ModelState.IsValid)
            {
                return View(departmentVM);
            }
            var massage = string.Empty;
            try
            {
                var Result = _departmentServices.UpdateDepartment(new UpdateDepartmentDto()
                {
                    Id = id,
                    Name = departmentVM.Name,
                    Code = departmentVM.Code,
                    Discription = departmentVM.Description ?? string.Empty,
                    DateofCreation = departmentVM.DateofCreation
                });
                if (Result > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    massage = "Department Can't be Updated";
                }
            }
            catch (Exception ex)
            {
                massage = _env.IsDevelopment() ? ex.Message : "Department Can't be Updated";
            }
            return View(departmentVM);
        }
        #endregion

        #region Delete

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id is null || id <= 0)
            {
                return BadRequest("Invalid department ID."); // 400 Bad Request 
            }
            var department = _departmentServices.GetDepartmentById(id.Value);
            if (department is null)
            {
                return NotFound("Department not found."); // 404 Not Found
            }
            return View(department);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var massage = string.Empty;
            try
            {
                var Result = _departmentServices.DeleteDepartment(id);
                if (Result)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    massage = "Department Can't be Deleted";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                massage = _env.IsDevelopment() ? ex.Message : "Department Can't be Deleted";
            }
            return View(nameof(Index));
        }
        #endregion
    }
}
