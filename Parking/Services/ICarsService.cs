using Parking.Models;

namespace Parking.Services;

public interface ICarsService
{
    Task<IEnumerable<Car>> GetAllCarsAsync();
    Task<List<Car>> GetAllCarsByOwnerIdAsync(int ownerId);
    Task<Car> GetCarByIdAsync(int? id);
    Task<Car> GetCarByNumberAsync(string number);
    Task<Car> CreateCarAsync(Car car);
    Task<Car> ChangeParkingStatus(int carId, bool value);
    Task<Car> UpdateCarAsync(Car car);
    Task DeleteCarByIdAsync(int id);
    Task DeleteCarByNumberAsync(string number);
}