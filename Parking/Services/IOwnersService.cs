using Parking.Models;

namespace Parking.Services;

public interface IOwnersService
{
    Task<Owner> GetOwnerByIdAsync(int? id);
    Task<IList<Owner>> GetAllOwnersAsync();
    Task<Owner> UpdateOwner(Owner owner);
    Task<Owner> CreateOwner(Owner owner);
    Task DeleteOwnerByIdAsync(int id);
}