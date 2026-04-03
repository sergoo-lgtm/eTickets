using eTickets.Models;
using eTickets.UnitOfWork;
using eTickets.DTO.ActorDTOS; 

namespace eTickets.Service;

public class ActorService
{
    private readonly IUnitOfWork _unitOfWork;

    public ActorService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public IEnumerable<ActorDto> GetAllActors()
    {
        var actors = _unitOfWork.Actors.GetAll.ToList();
        
        return actors.Select(a => new ActorDto
        {
            Id = a.Id,
            FullName = a.FullName,
            ProfilePicture = a.ProfilePivture, 
            Bio = a.Bio
        });
    }

    public async Task<ActorDto> GetActorAsync(int id)
    {
        var actor = await _unitOfWork.Actors.GetByIdAsync(id);
        
        if (actor == null) return null;

        return new ActorDto
        {
            Id = actor.Id,
            FullName = actor.FullName,
            ProfilePicture = actor.ProfilePivture,
            Bio = actor.Bio
        };
    }

    public async Task<ActorDto> AddActorAsync(ActorInputDto dto)
    {
        var newActor = new Actor(dto.FullName, dto.ProfilePicture, dto.Bio);

        await _unitOfWork.Actors.AddAsync(newActor);
        await _unitOfWork.SaveChangesAsync();

        return new ActorDto
        {
            Id = newActor.Id,
            FullName = newActor.FullName,
            ProfilePicture = newActor.ProfilePivture,
            Bio = newActor.Bio
        };
    }

    public async Task<ActorDto> UpdateAsync(int id, ActorInputDto dto)
    {
        var existingActor = await _unitOfWork.Actors.GetByIdAsync(id);

        if (existingActor == null)
            throw new Exception("Actor not found");

        existingActor.Update(dto.FullName, dto.ProfilePicture, dto.Bio);

        await _unitOfWork.Actors.UpdateAsync(existingActor);
        await _unitOfWork.SaveChangesAsync();

        return new ActorDto
        {
            Id = existingActor.Id,
            FullName = existingActor.FullName,
            ProfilePicture = existingActor.ProfilePivture,
            Bio = existingActor.Bio
        };
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var a = await _unitOfWork.Actors.GetByIdAsync(id); 
        if (a == null)
        {
            throw new NullReferenceException($"Actor with id {id} not found");
        }
        
        await _unitOfWork.Actors.RemoveAsync(id);
        await _unitOfWork.SaveChangesAsync();
        
        return true; 
    }
}