using Microsoft.EntityFrameworkCore;
using Parking.Db;
using Parking.Models;

namespace Parking.Services;

public class ParkingHistoryService : IParkingHistoryService
{
   // private readonly IParkingDetailsService _parkingDetailsService;
    private readonly IOwnersService _ownersService;
    private readonly AppDbContext _dbContext;

    public ParkingHistoryService(AppDbContext dbContext, 
        IOwnersService ownersService)
    {
        _dbContext = dbContext;
        _ownersService = ownersService;
        //_parkingDetailsService = parkingDetailsService;
    }

    public async Task<ParkingHistory> GetParkingHistoryByIdAsync(int? id)
    {
        var history = await _dbContext.ParkingHistories.Where(ph => ph.Id == id).FirstOrDefaultAsync();

        if (history == null)
        {
            throw new Exception("Parking history with such an id does not exist");
        }

        return history;
    }

    public async Task<ParkingHistory> GetParkingHistoryByOwnerIdAsync(int ownerId)
    {
        await _ownersService.GetOwnerByIdAsync(ownerId);

        var history = await _dbContext.ParkingHistories.Where(ph => ph.OwnerId == ownerId).FirstOrDefaultAsync();

        if (history == null)
        {
            throw new Exception("There is no parking history with such OwnerId");
        }

        return history;
    }

    public async Task DeleteParkingHistoryByIdAsync(int id)
    {
        await using var transaction = await _dbContext.Database.BeginTransactionAsync();
        
        var history = await GetParkingHistoryByIdAsync(id);
        
        _dbContext.ParkingDetails.RemoveRange(_dbContext.ParkingDetails.FromSqlRaw($"SELECT * FROM ParkingDetails WHERE ParkingHistoryId = {id}"));

        _dbContext.ParkingHistories.Remove(history);
        await _dbContext.SaveChangesAsync();
        
        await _dbContext.Database.CommitTransactionAsync();
        
    }
}