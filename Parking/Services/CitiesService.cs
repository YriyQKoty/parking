using Microsoft.EntityFrameworkCore;
using Parking.Db;
using Parking.Models;

namespace Parking.Services;

public class CitiesService : ICitiesService
{
    private readonly AppDbContext _dbContext;

    public CitiesService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IList<City>> GetAllCitiesAsync()
    {
        return await _dbContext.Cities.FromSqlRaw("SELECT * FROM CITIES").ToListAsync();
    }

    public async Task<City> GetCityByIdAsync(int? id)
    {
        var city = await _dbContext.Cities.Where(c => c.Id == id).FirstOrDefaultAsync();
        if (city == null)
        {
            throw new Exception("City Entity was not found with such an id");
        }

        return city;
    }
    
    public async Task<City> GetCityByNameAsync(string name)
    {
        var city = await _dbContext.Cities.Where(c => c.Name == name).FirstOrDefaultAsync();
        if (city == null)
        {
            throw new Exception("City Entity was not found with such a name");
        }

        return city;
    }

    public async Task DeleteCityById(int id)
    {
        var city = await GetCityByIdAsync(id);

        _dbContext.Cities.Remove(city);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<City> CreateCity(City city)
    {
        if (await _dbContext.Cities.Where(c => c.Name == city.Name).FirstOrDefaultAsync() != null)
        {
            throw new Exception("City Entity already exists with such a name");
        }
        
        var created = (await _dbContext.Cities.AddAsync(new City() { Name = city.Name })).Entity;
       await _dbContext.SaveChangesAsync();

       return created;
    }
    
}