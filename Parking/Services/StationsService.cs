using Microsoft.EntityFrameworkCore;
using Parking.Db;
using Parking.Models;

namespace Parking.Services;

public class StationsService : IStationsService
{
    private readonly AppDbContext _dbContext;
    private readonly ICitiesService _citiesService;

    public StationsService(AppDbContext dbContext, ICitiesService citiesService)
    {
        _dbContext = dbContext;
        _citiesService = citiesService;
    }

    public async Task<Station> GetStationByIdAsync(int? id)
    {
        var station = await _dbContext.Stations.Where(s => s.Id == id).FirstOrDefaultAsync();

        if (station == null)
        {
            throw new Exception("Station with such an  id does not exist");
        }

        return station;
    }

    public async Task<Station> GetStationByNameAsync(string name)
    {
        var station = await _dbContext.Stations.Where(s => s.Name == name).FirstOrDefaultAsync();

        if (station == null)
        {
            throw new Exception("Station with such a name does not exist");
        }

        return station;
    }

    public async Task<IList<Station>> GetAllStationsAsync()
    {
        return await _dbContext.Stations.FromSqlRaw("SELECT * FROM STATIONS").ToListAsync();
    }

    public async Task<IList<Station>> GetAllStationsInCityAsync(string cityName)
    {
        var city = await _citiesService.GetCityByNameAsync(cityName);
        return await _dbContext.Stations.FromSqlRaw($"SELECT * FROM STATION WHERE {city.Name}").ToListAsync();
    }

    public async Task<Station> CreateStationAsync(Station station)
    {
        //check if city exists
        await _citiesService.GetCityByIdAsync(station.CityId);
        
        //check if station with the same name and city id exists
        var stat = await _dbContext.Stations.Where(s => s.Name == station.Name && s.CityId == station.CityId)
            .FirstOrDefaultAsync();

        if (stat != null)
        {
            throw new Exception("Station with such name in chosen city already exists. Try another name.");
        }
            
        var s = await _dbContext.Stations.AddAsync(new Station()
        {
            Name = station.Name,
            CityId = station.CityId,
            PlacesCount = station.PlacesCount,
            CarsCount = 0,
            PricePerHour = station.PricePerHour
        });

        await _dbContext.SaveChangesAsync();
        return s.Entity;
    }

    public async Task<Station> UpdateStationAsync(Station station)
    {
        //check if city exists
        await _citiesService.GetCityByIdAsync(station.CityId);
        
        //check if station exists
        var s = await GetStationByIdAsync(station.Id);

        s.Name = station.Name;
        s.CityId = station.CityId;
        s.PlacesCount = station.PlacesCount;
        s.PricePerHour = station.PricePerHour;

        _dbContext.Stations.Update(s);

        await _dbContext.SaveChangesAsync();

        return s;
    }

    public async Task DeleteStationById(int id)
    {
        var station = await GetStationByIdAsync(id);

        _dbContext.Stations.Remove(station);
        await _dbContext.SaveChangesAsync();
    }
}