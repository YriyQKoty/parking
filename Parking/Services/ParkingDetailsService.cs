using Microsoft.EntityFrameworkCore;
using Parking.Db;
using Parking.Models;

namespace Parking.Services;

public class ParkingDetailsService : IParkingDetailsService
{
    private readonly ICarsService _carsService;
    private readonly IStationsService _stationsService;
    private readonly IParkingHistoryService _parkingHistoryService;
    private readonly AppDbContext _dbContext;

    public ParkingDetailsService(AppDbContext dbContext, ICarsService carsService,
        IStationsService stationsService, 
        IParkingHistoryService parkingHistoryService)
    {
        _dbContext = dbContext;
        
        _carsService = carsService;
        _stationsService = stationsService;
        _parkingHistoryService = parkingHistoryService;
    }

    public async Task<ParkingDetail> GetParkingDetailsByIdAsync(int id)
    {
        var pd = await _dbContext.ParkingDetails.Where(p => p.Id == id).FirstOrDefaultAsync();

        if (pd == null)
        {
            throw new Exception("ParkingDetails with such an id does not exist");
        }

        return pd;
    }
    
    public async Task<IList<ParkingDetail>> GetAllParkingDetailsAsync()
    {
        return await _dbContext.ParkingDetails.FromSqlRaw("SELECT * FROM ParkingDetails").ToListAsync();
    }

    public async Task<IList<ParkingDetail>> GetAllParkingDetailsByStationIdAsync(int stationId)
    {
        //check if station exists
        await _stationsService.GetStationByIdAsync(stationId);

        return await _dbContext.ParkingDetails
            .FromSqlRaw($"SELECT * FROM ParkingDetails WHERE Id = {stationId}")
            .ToListAsync();
    }
    
    public async Task<IList<ParkingDetail>> GetAllParkingDetailsByParkingHistoryIdAsync(int parkingHistoryId)
    {
        //check if parking history exists
        await _parkingHistoryService.GetParkingHistoryByIdAsync(parkingHistoryId);

        return await _dbContext.ParkingDetails
            .FromSqlRaw($"SELECT * FROM ParkingDetails WHERE ParkingHistoryId = {parkingHistoryId}")
            .ToListAsync();
    }
    
    public async Task<IList<ParkingDetail>> GetAllParkingDetailsByCarNumberAsync(string number)
    {
        //check if car exists
        var car = await _carsService.GetCarByNumberAsync(number);

        return await _dbContext.ParkingDetails
            .FromSqlRaw($"SELECT * FROM ParkingDetails WHERE CarId = {car.Id}")
            .ToListAsync();
    }
    
        
    public async Task<IList<ParkingDetail>> GetAllParkingDetailsByCarIdAsync(int carId)
    {
        //check if car exists
        var car = await _carsService.GetCarByIdAsync(carId);

        return await _dbContext.ParkingDetails
            .FromSqlRaw($"SELECT * FROM ParkingDetails WHERE CarId = {carId}")
            .ToListAsync();
    }

    public async Task DeleteParkingDetailsById(int id)
    {
        var pd = await GetParkingDetailsByIdAsync(id);

         _dbContext.ParkingDetails.Remove(pd);

         await _dbContext.SaveChangesAsync();

         await _dbContext.Entry(pd).ReloadAsync();
    }

    public async Task<ParkingDetail> CreateParkingDetailsAsync(ParkingDetail parkingDetail)
    {
        //check if car exists
        var c = await _carsService.GetCarByIdAsync(parkingDetail.CarId);

        //check if station exists
        var s = await _stationsService.GetStationByIdAsync(parkingDetail.StationId);

        //check if parking history exists
        await _parkingHistoryService.GetParkingHistoryByIdAsync(parkingDetail.ParkingHistoryId);
        
        var pd = await _dbContext.ParkingDetails.AddAsync(new ParkingDetail()
        {
            CarId = parkingDetail.CarId,
            StationId = parkingDetail.StationId,
            ParkingHistoryId = parkingDetail.ParkingHistoryId,
            
            Bill = parkingDetail.Bill,
            
            BookTime = parkingDetail.BookTime,
            ExpireTime = parkingDetail.ExpireTime
        });
        
        await _dbContext.SaveChangesAsync();

        await _dbContext.Entry(c).ReloadAsync();
        await _dbContext.Entry(s).ReloadAsync();
        
        return pd.Entity;
    }

    public async Task<ParkingDetail> UpdateParkingDetailAsync(ParkingDetail parkingDetail)
    {
        await using var transaction = await _dbContext.Database.BeginTransactionAsync(); 
        
        //check if Parking details exists
        var pd = await GetParkingDetailsByIdAsync(parkingDetail.Id);
        
        //check if car exists
        await _carsService.GetCarByIdAsync(parkingDetail.CarId);

        //check if station exists
        await _stationsService.GetStationByIdAsync(parkingDetail.StationId);

        //check if parking history exists
       await _parkingHistoryService.GetParkingHistoryByIdAsync(parkingDetail.ParkingHistoryId);

       pd.ExpireTime = parkingDetail.ExpireTime;
       pd.BookTime = parkingDetail.BookTime;
       
       pd.StationId = parkingDetail.StationId;
       pd.CarId = parkingDetail.CarId;
       pd.ParkingHistoryId = parkingDetail.ParkingHistoryId;
       
       pd.Bill = parkingDetail.Bill;

       _dbContext.ParkingDetails.Update(pd);

       await _dbContext.SaveChangesAsync();
       
       await _dbContext.Database.CommitTransactionAsync();

       return pd;
    }
}