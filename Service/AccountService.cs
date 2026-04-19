using eTickets.DTO;
using eTickets.Models;
using eTickets.UnitOfWork;

namespace eTickets.Service;



public class AccountService 
{
    private readonly IUnitOfWork _unitOfWork;

    public AccountService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> RegisterAsync(RegisterDTO dto)
    {
        var exists = _unitOfWork.Users
            .GetAll
            .Any(u => u.Email == dto.Email);

        if (exists)
            return false;

        var user = new AppUser(
            dto.FullName,
            dto.Email,
            dto.Password,
            "User"
        );

        await _unitOfWork.Users.AddAsync(user);
        await _unitOfWork.SaveChangesAsync();

        return true;
    }

    public async Task<UserDTO?> LoginAsync(LoginDTO dto)
    {
        var user = _unitOfWork.Users
            .GetAll
            .FirstOrDefault(u =>
                u.Email == dto.Email &&
                u.Password == dto.Password);

        if (user == null)
            return null;

        return new UserDTO
        {
            Id = user.Id,
            FullName = user.FullName,
            Email = user.Email,
            Role = user.Role ?? "User"
        };
    }
}
