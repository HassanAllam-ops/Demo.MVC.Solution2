using Demo.BusnissLogic.DataTransferObjects.Empolyees;
using Demo.BusnissLogic.Services.Interfaces;
using Demo.DataAccess.Models.Empolyees;
using Demo.PresentationLayer.ViewModels.Empolyees;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Demo.PresentationLayer.Controllers
{
    [Authorize]
    public class EmpolyeesController : Controller
    {
        private readonly IEmpolyeeServices _empolyeeServices;
        private readonly ILogger<EmpolyeesController> _logger;
        private readonly IWebHostEnvironment _env;
        public EmpolyeesController(IEmpolyeeServices empolyeeServices,
                      ILogger<EmpolyeesController> logger,
                      IWebHostEnvironment env)
        {
            _empolyeeServices = empolyeeServices;
            _logger = logger;
            _env = env;
        }
        // Master Action
        public IActionResult Index(string? EmployeeSearchName)
        {
            var empolyees = _empolyeeServices.GetAllEmpolyees(EmployeeSearchName);
            return View(empolyees);
        }

        #region Create
        // Get : baseUrl/Empolyees/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(EmpolyeeViewModel empolyeeVM)
        {
            // Server Side Validation
            if (ModelState.IsValid)
            {
                try
                {
                    var empolyeeDto = new CreatedEmpolyeeDto()
                    {
                        Name = empolyeeVM.Name,
                        Address = empolyeeVM.Address,
                        Age = empolyeeVM.Age,
                        Salary = empolyeeVM.Salary,
                        IsActive = empolyeeVM.IsActive,
                        Email = empolyeeVM.Email,
                        PhoneNumber = empolyeeVM.PhoneNumber,
                        HiringDate = empolyeeVM.HiringDate,
                        Gender = empolyeeVM.Gender,
                        EmpolyeeType = empolyeeVM.EmpolyeeType,
                        DepartmentId = empolyeeVM.DepartmentId,
                        Image = empolyeeVM.Image
                    };
                        var result = _empolyeeServices.CreateEmpolyee(empolyeeDto);
                    if (result > 0)
                        return RedirectToAction(nameof(Index));
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Can't Created Empolyee Right Now");
                    }
                }
                catch (Exception ex)
                {
                    if (_env.IsDevelopment())
                    {
                        ModelState.AddModelError(string.Empty, ex.Message);
                    }
                    else
                    {
                        _logger.LogError(ex.Message);
                    }
                }
            }
            return View(empolyeeVM);
        }
        #endregion

        // Get : baseUrl/Empolyees/Details/{id}
        public IActionResult Details (int? id)
        {
            if (id is null || id <= 0)
                return BadRequest(); // 400
            var empolyee = _empolyeeServices.GetEmpolyeeById(id.Value);
            if (empolyee is null)
                return NotFound(); // 404

            return View(empolyee);
        }

        #region Edit
        // Get : baseUrl/Empolyees/Edit/{id}
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id is null || id <= 0)
                return BadRequest(); // 400
            var empolyee = _empolyeeServices.GetEmpolyeeById(id.Value);
            if (empolyee is null)
                return NotFound(); // 404
            return View(new EmpolyeeViewModel()
            {
                Name = empolyee.Name,
                Address = empolyee.Address,
                Age = empolyee.Age,
                Salary = empolyee.Salary,
                IsActive = empolyee.IsActive,
                Email = empolyee.Email,
                PhoneNumber = empolyee.PhoneNumber,
                HiringDate = empolyee.HiringDate,
                Gender = Enum.Parse<Gender>(empolyee.Gender),
                EmpolyeeType = Enum.Parse<EmpolyeeType>(empolyee.EmpolyeeType),
                DepartmentId = empolyee.DepartmentId
            });
        }

        [HttpPost]
        //[ValidateAntiForgeryToken] // Action Filter
        public IActionResult Edit([FromRoute] int? id, EmpolyeeViewModel empolyeeVM)
        {
            if (id is null) return BadRequest(); // 400
            if (ModelState.IsValid)
            {
                try
                {
                    var empolyeeDto = new UpdatedEmpolyeeDto()
                    {
                        Id = id.Value,
                        Name = empolyeeVM.Name,
                        Address = empolyeeVM.Address,
                        Age = empolyeeVM.Age,
                        Salary = empolyeeVM.Salary,
                        IsActive = empolyeeVM.IsActive,
                        Email = empolyeeVM.Email,
                        PhoneNumber = empolyeeVM.PhoneNumber,
                        HiringDate = empolyeeVM.HiringDate,
                        Gender = empolyeeVM.Gender,
                        EmpolyeeType = empolyeeVM.EmpolyeeType,
                        DepartmentId = empolyeeVM.DepartmentId,
                        Image = empolyeeVM.Image
                    }; 
                        var result = _empolyeeServices.UpdateEmpolyee(empolyeeDto);
                    if (result > 0)
                        return RedirectToAction(nameof(Index));
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Empolyee is not Updated");
                    }
                }
                catch (Exception ex)
                {
                    if (_env.IsDevelopment())
                    {
                        ModelState.AddModelError(string.Empty, ex.Message);
                    }
                    else
                    {
                        _logger.LogError(ex.Message);
                    }
                }
            }
            return View(empolyeeVM);
        }
        #endregion

        [HttpPost]
        public IActionResult Delete([FromRoute]int? id)
        {
            if (id is null) return BadRequest(); // 400
            try
            {
                var result = _empolyeeServices.DeleteEmpolyee(id.Value);
                if (result)
                    return RedirectToAction(nameof(Index));
                else
                    _logger.LogError("Empolyee Can't be Deleted");
            }
            catch (Exception ex)
            {
                if (_env.IsDevelopment())
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
                else
                {
                    _logger.LogError(ex.Message);
                }
            }
            return RedirectToAction(nameof(Index));
        }

    }
}
