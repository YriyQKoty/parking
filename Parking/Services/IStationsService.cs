using Parking.Models;

namespace Parking.Services;

public interface IStationsService
{
    Task<Station> GetStationByIdAsync(int? id);
    Task<Station> GetStationByNameAsync(string name);
    Task<IList<Station>> GetAllStationsAsync();
    Task<IList<Station>> GetAllStationsInCityAsync(string cityName);
    Task<Station> CreateStationAsync(Station station);
    Task<Station> UpdateStationAsync(Station station);
    Task DeleteStationById(int id);
}