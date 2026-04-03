using eTickets.Data;
using Microsoft.AspNetCore.Mvc;

namespace eTickets.Controllers;

public class CinemasController : Controller
{
    private readonly AppDbContext context;
    public CinemasController(AppDbContext context)
    {
        this.context = context; 
        
    }    
    public IActionResult Index()
    {
        var allcinemas = context.Cinemas.ToList();
        return View(allcinemas);
    }
}