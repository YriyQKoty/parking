using Parking.Models;

namespace Parking.Services;

public interface ICitiesService
{
    Task<IList<City>> GetAllCitiesAsync();
    Task<City> GetCityByIdAsync(int? id);
    Task<City> GetCityByNameAsync(string name);
    Task DeleteCityById(int id);
    Task<City> CreateCity(City city);
}