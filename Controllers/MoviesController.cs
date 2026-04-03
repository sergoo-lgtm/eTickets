using eTickets.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Controllers;

public class MoviesController : Controller
{
    private readonly AppDbContext context;
    public MoviesController(AppDbContext context)
    {
        this.context = context; 
        
    }
    public IActionResult Index()
    {
        var movies = context.Movies.Include(n=>n.Cinema).OrderBy(n=>n.Name).ToList();
        return View(movies);
    }
}