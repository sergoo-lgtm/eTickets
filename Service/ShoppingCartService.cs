using eTickets.Data;
using eTickets.Extensions;
using eTickets.Models;
using eTickets.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Service;

public class ShoppingCartService
{
    private const string CartSessionKey = "shopping_cart_items";
    private readonly AppDbContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ShoppingCartService(AppDbContext context, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task AddItemAsync(int movieId)
    {
        var movie = await _context.Movies
            .AsNoTracking()
            .Where(m => m.Id == movieId)
            .Select(m => new
            {
                m.Id,
                m.Name,
                m.ImageURL,
                m.Price
            })
            .FirstOrDefaultAsync();

        if (movie == null)
            throw new KeyNotFoundException($"Movie with ID {movieId} not found.");

        var items = GetCartItems();
        var existingItem = items.FirstOrDefault(i => i.MovieId == movieId);

        if (existingItem == null)
        {
            items.Add(new ShoppingCartItem
            {
                MovieId = movie.Id,
                MovieName = movie.Name,
                ImageUrl = movie.ImageURL,
                Price = Convert.ToDecimal(movie.Price),
                Quantity = 1
            });
        }
        else
        {
            existingItem.Quantity++;
        }

        SaveCartItems(items);
    }

    public void RemoveItem(int movieId)
    {
        var items = GetCartItems();
        var existingItem = items.FirstOrDefault(i => i.MovieId == movieId);

        if (existingItem == null)
            return;

        existingItem.Quantity--;

        if (existingItem.Quantity <= 0)
        {
            items.Remove(existingItem);
        }

        SaveCartItems(items);
    }

    public List<ShoppingCartItem> GetCartItems()
    {
        var session = GetSession();
        return session.GetObject<List<ShoppingCartItem>>(CartSessionKey) ?? new List<ShoppingCartItem>();
    }

    public int GetCartCount()
    {
        return GetCartItems().Sum(item => item.Quantity);
    }

    public ShoppingCartViewModel GetCart()
    {
        var items = GetCartItems()
            .OrderBy(item => item.MovieName)
            .ToList();

        return new ShoppingCartViewModel
        {
            Items = items,
            Total = items.Sum(item => item.LineTotal)
        };
    }

    private void SaveCartItems(List<ShoppingCartItem> items)
    {
        var session = GetSession();
        session.SetObject(CartSessionKey, items);
    }

    private ISession GetSession()
    {
        var session = _httpContextAccessor.HttpContext?.Session;

        if (session == null)
            throw new InvalidOperationException("Session is not available for the current request.");

        return session;
    }
}
