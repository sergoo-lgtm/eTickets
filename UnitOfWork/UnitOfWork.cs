using eTickets.Data;
using eTickets.Models;
using eTickets.Services;

namespace eTickets.UnitOfWork
{
    
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        public UnitOfWork(AppDbContext context)
        {
            _context = context;

            Actors = new GenericRepository<Actor>(_context);
            Cinemas = new GenericRepository<Cinema>(_context);
            Producers = new GenericRepository<Producer>(_context);
            Movies = new GenericRepository<Movie>(_context);
            Users = new GenericRepository<AppUser>(_context);
        
        }
        public IGenericRepository<Actor> Actors { get; }
        public IGenericRepository<Cinema> Cinemas { get; }
        public IGenericRepository<Producer> Producers { get; }
        public IGenericRepository<Movie> Movies { get; }
        public IGenericRepository<AppUser> Users { get; }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}