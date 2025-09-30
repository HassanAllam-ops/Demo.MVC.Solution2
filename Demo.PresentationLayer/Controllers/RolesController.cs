using Demo.DataAccess.Models.IdentityModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Demo.PresentationLayer.Controllers
{
    public class RolesController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<RolesController> _logger;
        private readonly IWebHostEnvironment _env;

        public RolesController(RoleManager<IdentityRole> roleManager,
               ILogger<RolesController> logger,
               IWebHostEnvironment env)

        {
            _roleManager = roleManager;
            _logger = logger;
            _env = env;
        }

        public IActionResult Index(string? RoleSearcheName)
        {
            var roles = _roleManager.Roles.AsQueryable();

            if (!string.IsNullOrEmpty(RoleSearcheName))
            {
                roles = roles.Where(u => u.Name != null && u.Name.Contains(RoleSearcheName));
            }
            var rolesList = roles.ToList();
            ViewBag.UserSearchName = RoleSearcheName;
            return View(rolesList);
        }

        #region Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(IdentityRole role)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(role.Name))
                {
                    ModelState.AddModelError(nameof(role.Name), "Role name is required.");
                    return View(role);
                }
                var existingRole = _roleManager.FindByNameAsync(role.Name).Result;
                if (existingRole != null)
                {
                    ModelState.AddModelError("", "Role already exists.");
                    return View(role);
                }
                var result = _roleManager.CreateAsync(new IdentityRole { Name = role.Name }).Result;
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(role);
        }
        #endregion

        public IActionResult Details(string id)
        {
            if (id is null)
                return BadRequest(); // 400
            var role = _roleManager.FindByIdAsync(id).Result;
            if (role is null)
            {
                return NotFound(); // 404
            }
            return View(role);
        }

        #region Edit
        [HttpGet]
        public IActionResult Edit(string id)
        {
            if (id is null)
                return BadRequest(); // 400
            var role = _roleManager.FindByIdAsync(id).Result;
            if (role is null)
            {
                return NotFound(); // 404
            }
            return View(new IdentityRole
            {
                Id = role.Id,
                Name = role.Name
            });
        }

        [HttpPost]
        public IActionResult Edit([FromRoute] string? id, IdentityRole identityRole)
        {
            if (id is null)
                return BadRequest(); // 400
            if (ModelState.IsValid)
            {
                try
                {
                    var role = _roleManager.FindByIdAsync(id).Result;
                    if (role is null)
                        return NotFound(); // 404

                    role.Id = id;
                    role.Name = identityRole.Name;

                    var result = _roleManager.UpdateAsync(role).Result;
                    if (result.Succeeded)
                        return RedirectToAction(nameof(Index));
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Role is not Updated");
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
            return View(identityRole);
        }
        #endregion

        #region Delete
        [HttpGet]
        public IActionResult Delete(string id)
        {
            if (id is null)
                return BadRequest(); // 400
            var role = _roleManager.FindByIdAsync(id).Result;
            if (role is null)
            {
                return NotFound(); // 404
            }
            return View(role);
        }

        [HttpPost]
        public IActionResult Delete([FromRoute] string? id, IdentityRole identityRole)
        {
            if (id is null)
                return BadRequest(); // 400
            var massage = string.Empty;
            try
            {
                var role = _roleManager.FindByIdAsync(id).Result;
                if (role is null)
                    return NotFound(); // 404
                var result = _roleManager.DeleteAsync(role).Result;
                if (result.Succeeded)
                    return RedirectToAction(nameof(Index));
                else
                {
                    massage = "Role Can't be Deleted";
                }
            }
            catch (Exception ex)
            {
                massage = _env.IsDevelopment() ? ex.Message : "Role Can't be Deleted";
            }
            ViewBag.Massage = massage;
            return View(identityRole);
            #endregion
        }
    }
}
