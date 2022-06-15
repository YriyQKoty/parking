using Microsoft.EntityFrameworkCore;
using Parking.Db;
using Parking.Models;

namespace Parking.Services;

public class OwnersService : IOwnersService
{
    private readonly AppDbContext _dbContext;

    public OwnersService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Owner> GetOwnerByIdAsync(int? id)
    {
        var owner = await _dbContext.Owners.Where(o => o.Id == id).FirstOrDefaultAsync();

        if (owner == null)
        {
            throw new Exception("Owner Entity with such an id does not exist");
        }

        return owner;
    }

    public async Task<IList<Owner>> GetAllOwnersAsync()
    {
        return await _dbContext.Owners.FromSqlRaw("SELECT * FROM OWNERS").ToListAsync();
    }

    public async Task<Owner> UpdateOwner(Owner owner)
    {
        var o = await GetOwnerByIdAsync(owner.Id);

        o.Birthdate = owner.Birthdate;
        o.Surname = owner.Surname;
        o.PhoneNumber = owner.PhoneNumber;

        _dbContext.Owners.Update(o);
        await _dbContext.SaveChangesAsync();

        return o;
    }

    public async Task<Owner> CreateOwner(Owner owner)
    {
       var o = await _dbContext.Owners.AddAsync(new Owner()
       {
           Surname = owner.Surname,
           PhoneNumber = owner.PhoneNumber, 
           Birthdate = owner.Birthdate
       });
       
        await _dbContext.SaveChangesAsync();
        
        return o.Entity;
    }

    public async Task DeleteOwnerByIdAsync(int id)
    {
        var owner = await GetOwnerByIdAsync(id);

        _dbContext.Owners.Remove(owner);
        
        await _dbContext.SaveChangesAsync();
    }
 }