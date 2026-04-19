using eTickets.Data;
using Microsoft.AspNetCore.Mvc;

namespace eTickets.Controllers;

public class ProducersController : Controller
{
    private readonly AppDbContext context;
    public ProducersController(AppDbContext context)
    {
        this.context = context; 
        
    }
    public IActionResult Index()
    {
        var data = context.Producers.ToList();
        return View(data);
    }
}