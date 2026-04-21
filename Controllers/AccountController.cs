using eTickets.DTO;
using eTickets.Models.IdentityEntities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace eTickets.Controllers
{
    [Route("[controller]/[action]")]

    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public AccountController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View("Register");
        }


        [HttpPost]
        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {
            if (ModelState.IsValid == false)
            {
                ViewBag.Errors = ModelState.Values.SelectMany(Temp => Temp.Errors).Select(temp => temp.ErrorMessage);
                return View(registerDTO);
            }
            ApplicationUser user = new ApplicationUser()
            {
                Email = registerDTO.Email,
                PhoneNumber = registerDTO.Phone,
                UserName = registerDTO.Email,
                PersonName = registerDTO.PersonName,

            };
           IdentityResult result = await _userManager.CreateAsync(user);
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(MoviesController.Index), "Movies");
            }

            else
            {
               foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("Register", error.Description);
                }
                ViewBag.Errors = ModelState.Values.SelectMany(Temp => Temp.Errors).Select(temp => temp.ErrorMessage);
                return View(registerDTO);
            }
        }
    }
}
