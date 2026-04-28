using Microsoft.AspNetCore.Authorization;
using eTickets.Service;
using Microsoft.AspNetCore.Mvc;

namespace eTickets.Controllers;

[Authorize]
public class ShoppingCartController : Controller
{
    private readonly ShoppingCartService _shoppingCartService;

    public ShoppingCartController(ShoppingCartService shoppingCartService)
    {
        _shoppingCartService = shoppingCartService;
    }

    public IActionResult Index()
    {
        var cart = _shoppingCartService.GetCart();
        return View(cart);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddToCart(int id, string? returnUrl)
    {
        try
        {
            await _shoppingCartService.AddItemAsync(id);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }

        if (!string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl))
            return Redirect(returnUrl);

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult RemoveFromCart(int id)
    {
        _shoppingCartService.RemoveItem(id);
        return RedirectToAction(nameof(Index));
    }
}
