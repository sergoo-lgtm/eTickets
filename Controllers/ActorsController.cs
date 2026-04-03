using eTickets.Models;
using eTickets.Service;
using eTickets.DTO.ActorDTOS; 
using Microsoft.AspNetCore.Mvc;

namespace eTickets.Controllers;

public class ActorsController : Controller
{
    private readonly ActorService _service;
    public ActorsController(ActorService service)
    {
        _service = service; 
    }

    public IActionResult Index()
    {
        var data = _service.GetAllActors();
        return View(data);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(ActorInputDto actorDto) 
    {
        if (!ModelState.IsValid)
        {
            return View(actorDto);
        }

        await _service.AddActorAsync(actorDto);
        
        return RedirectToAction(nameof(Index));
    }
}