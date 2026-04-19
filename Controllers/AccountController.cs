using System.Security.Claims;
using eTickets.DTO;
using eTickets.Service;
using eTickets.Services;
using eTickets.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace eTickets.Controllers;

public class AccountController : Controller
{
    private readonly AccountService _accountService;
    public AccountController(AccountService accountService)
    { _accountService = accountService; }
    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterVM model)
    {
        if (!ModelState.IsValid)
            return View(model);
        var dto = new RegisterDTO
        {
            FullName = model.FullName,
            Email = model.Email,
            Password = model.Password
        };
        var result = await _accountService.RegisterAsync(dto);
        if (!result)
        {
            ModelState.AddModelError("", "Email already exists");
            return View(model);
        }
        return RedirectToAction("Login");
    }
    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginVM model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var dto = new LoginDTO
        {
            Email = model.Email,
            Password = model.Password
        };
        var user = await _accountService.LoginAsync(dto);

        if (user == null)
        {
            ModelState.AddModelError("", "Invalid email or password");
            return View(model);
        }
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.FullName),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role),
            new Claim("UserId", user.Id.ToString())
        };
        var identity = new ClaimsIdentity(
            claims,
            CookieAuthenticationDefaults.AuthenticationScheme
        );
        var principal = new ClaimsPrincipal(identity);
        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            principal
        );
        return RedirectToAction("Index", "Movies");
    }
    [HttpGet]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(
            CookieAuthenticationDefaults.AuthenticationScheme
        );
        return RedirectToAction("Index", "Movies");
    }
}