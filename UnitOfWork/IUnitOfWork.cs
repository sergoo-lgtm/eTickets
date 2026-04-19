using eTickets.Models;
using eTickets.Services;

namespace eTickets.UnitOfWork;


public interface IUnitOfWork
{
    IGenericRepository<Actor> Actors { get; }
    IGenericRepository<Cinema> Cinemas { get; }
    IGenericRepository<Producer> Producers { get; }
    IGenericRepository<Movie> Movies { get; }
    IGenericRepository<AppUser> Users { get; }
        Task SaveChangesAsync();
}
