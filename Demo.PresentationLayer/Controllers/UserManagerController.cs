using Demo.DataAccess.Models.IdentityModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Demo.PresentationLayer.Controllers
{
    public class UserManagerController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<UserManagerController> _logger;
        private readonly IWebHostEnvironment _env;

        public UserManagerController(UserManager<ApplicationUser> userManager,
               ILogger<UserManagerController> logger,
               IWebHostEnvironment env)
        {
            _userManager = userManager;
            _logger = logger;
            _env = env;
        }
        public IActionResult Index(string? UserSearchName)
        {
            var users = _userManager.Users.AsQueryable();

            if (!string.IsNullOrEmpty(UserSearchName))
            {
                users = users.Where(u => u.FirstName != null && u.FirstName.Contains(UserSearchName));
            }
            var usersList = users.ToList();
            ViewBag.UserSearchName = UserSearchName;
            return View(usersList);
        }

        public IActionResult Details(string id)
        {
            if (id is null)
                return BadRequest(); // 400
            var user = _userManager.FindByIdAsync(id).Result;
            if (user is null)
            {
                return NotFound(); // 404
            }
            return View(user);
        }

        #region Edit
        [HttpGet]
        public IActionResult Edit(string id)
        {
            if (id is null)
                return BadRequest(); // 400
            var user = _userManager.FindByIdAsync(id).Result;
            if (user is null)
            {
                return NotFound(); // 404
            }
            return View(new ApplicationUser
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
            });
        }

        [HttpPost]
        public IActionResult Edit([FromRoute] string? id, ApplicationUser applicationUser)
        {
            if (id is null)
                return BadRequest(); // 400
            if (ModelState.IsValid)
            {
                try
                {
                    var user = _userManager.FindByIdAsync(id).Result;
                    if (user is null)
                        return NotFound(); // 404

                    user.Id = id;
                    user.FirstName = applicationUser.FirstName;
                    user.LastName = applicationUser.LastName;
                    user.PhoneNumber = applicationUser.PhoneNumber;

                    var result = _userManager.UpdateAsync(user).Result;
                    if (result.Succeeded)
                        return RedirectToAction(nameof(Index));
                    else
                    {
                        ModelState.AddModelError(string.Empty, "User is not Updated");
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
            return View(applicationUser);
        }
        #endregion

        [HttpGet]
        public IActionResult Delete(string id)
        {
            if (id is null)
                return BadRequest(); // 400
            var user = _userManager.FindByIdAsync(id).Result;
            if (user is null)
            {
                return NotFound(); // 404
            }
            return View(user);
        }

        [HttpPost]
        public IActionResult Delete([FromRoute] string? id, ApplicationUser applicationUser)
        {
            if (id is null)
                return BadRequest(); // 400
            var massage = string.Empty;
            try
            {
                var user = _userManager.FindByIdAsync(id).Result;
                if (user is null)
                    return NotFound(); // 404
                var result = _userManager.DeleteAsync(user).Result;
                if (result.Succeeded)
                    return RedirectToAction(nameof(Index));
                else
                {
                    massage = "User Can't be Deleted";
                }
            }
            catch (Exception ex)
            {
                massage = _env.IsDevelopment() ? ex.Message : "User Can't be Deleted";
            }
            ViewBag.Massage = massage;
            return View(applicationUser);
        }
    }
}
