using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Parking.Db;
using Parking.Models;

namespace Parking.Services;

public class CarsService : ICarsService
{
    private readonly IOwnersService _ownersService;
    private readonly AppDbContext _dbContext;

    public CarsService(AppDbContext dbContext, IOwnersService ownersService)
    {
        _dbContext = dbContext;
        _ownersService = ownersService;
    }

    public async Task<IEnumerable<Car>> GetAllCarsAsync()
    {
       return await _dbContext.Cars.FromSqlRaw("SELECT * FROM CARS").ToListAsync();
    }

    public async Task<List<Car>> GetAllCarsByOwnerIdAsync(int ownerId)
    {
        //check if owner exists
        await _ownersService.GetOwnerByIdAsync(ownerId);

        return await _dbContext.Cars.FromSqlRaw(@"SELECT * FROM CARS WHERE OwnerId = {1}", ownerId).ToListAsync();
    }

    public async Task<Car> GetCarByIdAsync(int? id)
    {
        var car = await _dbContext.Cars.Where(c => c.Id == id).FirstOrDefaultAsync();

        if (car == null)
        {
            throw new Exception("Car with such an Id was not found");
        }

        return car;
    }

    public async Task<Car> GetCarByNumberAsync(string number)
    {
        var car = await _dbContext.Cars.Where(c => c.Number.Equals(number)).FirstOrDefaultAsync();

        if (car == null)
        {
           throw new Exception("Car with such a number was not found");
        }

        return car;
    }

    public async Task<Car> CreateCarAsync(Car car)
    {
        await _ownersService.GetOwnerByIdAsync(car.OwnerId);

        var c = await _dbContext.Cars.AddAsync(new Car()
        {
            Number = car.Number,
            IsParked = false,
            OwnerId = car.OwnerId,
            SeatsCount = car.SeatsCount
        });

        await _dbContext.SaveChangesAsync();

        return c.Entity;
    }

    public async Task<Car> ChangeParkingStatus(int carId, bool value)
    {
        var c = await GetCarByIdAsync(carId);

        c.IsParked = value;
        
        _dbContext.Cars.Update(c);
        await _dbContext.SaveChangesAsync();

        return c;
    }

    public async Task<Car> UpdateCarAsync(Car car)
    {
        await _ownersService.GetOwnerByIdAsync(car.OwnerId);

        var c = await GetCarByIdAsync(car.Id);

        c.Number = car.Number;
        c.IsParked = car.IsParked;
        c.OwnerId = car.OwnerId;
        c.SeatsCount = car.SeatsCount;

        _dbContext.Cars.Update(c);
        await _dbContext.SaveChangesAsync();

        return c;
    }

    public async Task DeleteCarByIdAsync(int id)
    {
        var car = await GetCarByIdAsync(id);
        
        _dbContext.Cars.Remove(car);
        await _dbContext.SaveChangesAsync();
    }
    
    public async Task DeleteCarByNumberAsync(string number)
    {
        var car = await GetCarByNumberAsync(number);

        _dbContext.Cars.Remove(car);
        await _dbContext.SaveChangesAsync();
    }

}