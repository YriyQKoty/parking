using Parking.Models;

namespace Parking.Services;

public interface IParkingDetailsService
{
    Task<ParkingDetail> GetParkingDetailsByIdAsync(int id);
    Task<IList<ParkingDetail>> GetAllParkingDetailsAsync();
    Task<IList<ParkingDetail>> GetAllParkingDetailsByStationIdAsync(int stationId);
    Task<IList<ParkingDetail>> GetAllParkingDetailsByParkingHistoryIdAsync(int parkingHistoryId);
    Task<IList<ParkingDetail>> GetAllParkingDetailsByCarNumberAsync(string number);
    Task<IList<ParkingDetail>> GetAllParkingDetailsByCarIdAsync(int carId);
    Task DeleteParkingDetailsById(int id);
    Task<ParkingDetail> CreateParkingDetailsAsync(ParkingDetail parkingDetail);
    Task<ParkingDetail> UpdateParkingDetailAsync(ParkingDetail parkingDetail);
}