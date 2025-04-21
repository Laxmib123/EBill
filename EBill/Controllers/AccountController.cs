using EBill.Models.Data;
using EBill.Models.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EBill.Controllers
{
    public class AccountController : Controller
    {

        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public AccountController(SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM data)
        {
            var result = await _signInManager.PasswordSignInAsync(data.Email, data.Password, isPersistent: false, lockoutOnFailure: false);

            if (result.Succeeded)
            {

                return Json(new
                {
                    Status = "Success"
                });

            }
            else
            {
                return Json(new
                {
                    Status = "Failed",
                    Message = "Invalid Password or Email!!"
                });

            }

            //return Json(new
            //{
            //    Status = "Success"
            //});


        }

        [HttpGet]
        public async Task<IActionResult> Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(LoginVM info)//only admin can register the user...................
        {
            var user = new ApplicationUser { UserName = info.Email, Email = info.Email };

            var result = await _userManager.CreateAsync(user, info.Password);

            if (result.Succeeded)
            {
                var roleExists = await _roleManager.RoleExistsAsync("Staff");

                if (!roleExists)
                {
                    var role = new ApplicationRole { Name = "Staff" };
                    await _roleManager.CreateAsync(role);
                }

                //Assign Role to user..................
                await _userManager.AddToRoleAsync(user, "Staff");

                return Json(new
                {
                    Status = "Success"
                });

            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);

                }

                return Json(new
                {
                    Status = "Fail",
                    Message = "Unable to register!!"
                });
            }

        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
    }
}

