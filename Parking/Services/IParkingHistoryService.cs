using Parking.Models;

namespace Parking.Services;

public interface IParkingHistoryService
{
    Task<ParkingHistory> GetParkingHistoryByIdAsync(int? id);
    Task<ParkingHistory> GetParkingHistoryByOwnerIdAsync(int ownerId);
    Task DeleteParkingHistoryByIdAsync(int id);
}