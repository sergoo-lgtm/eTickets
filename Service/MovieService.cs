using eTickets.Data;
using eTickets.DTO.MovieDTOS;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Service;

public class MovieService
{
    private readonly AppDbContext _context;

    public MovieService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<MovieDto>> GetAllMoviesAsync()
    {
        return await _context.Movies
            .AsNoTracking()
            .Include(m => m.Cinema)
            .Include(m => m.Producer)
            .OrderBy(m => m.Name)
            .Select(m => new MovieDto
            {
                Id = m.Id,
                Name = m.Name,
                Description = m.Description,
                StartDate = m.StartDate,
                EndDate = m.EndDate,
                Price = m.Price,
                ImageURL = m.ImageURL,
                MovieCategory = m.MovieCategory,
                CinemaName = m.Cinema.Name,
                ProducerName = m.Producer.FullName
            })
            .ToListAsync();
    }

    public async Task<MovieDto> GetMovieDetailsAsync(int id)
    {
        var movie = await _context.Movies
            .AsNoTracking()
            .Include(m => m.Cinema)
            .Include(m => m.Producer)
            .Include(m => m.Actors_Movies)
            .ThenInclude(am => am.Actor)
            .Where(m => m.Id == id)
            .Select(m => new MovieDto
            {
                Id = m.Id,
                Name = m.Name,
                Description = m.Description,
                StartDate = m.StartDate,
                EndDate = m.EndDate,
                Price = m.Price,
                ImageURL = m.ImageURL,
                MovieCategory = m.MovieCategory,
                CinemaName = m.Cinema.Name,
                ProducerName = m.Producer.FullName,
                ActorNames = m.Actors_Movies
                    .Select(am => am.Actor.FullName)
                    .OrderBy(name => name)
                    .ToList()
            })
            .FirstOrDefaultAsync();

        if (movie == null)
            throw new KeyNotFoundException($"Movie with ID {id} not found.");

        return movie;
    }
}
