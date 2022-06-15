using Microsoft.AspNetCore.Mvc;
using Parking.Services;

namespace Parking.Controllers;

public class ParkingHistoryController : BaseController
{
    private readonly IParkingHistoryService _parkingHistoryService;

    public ParkingHistoryController(IParkingHistoryService parkingHistoryService)
    {
        _parkingHistoryService = parkingHistoryService;
    }

    [HttpGet("id/{id}")]
    public async Task<IActionResult> GetParkingHistoryById([FromRoute]int id)
    {
        return Ok(await _parkingHistoryService.GetParkingHistoryByIdAsync(id));
    }
    
    [HttpGet("owner/{ownerId}")]
    public async Task<IActionResult> GetParkingHistoryByOwnerId([FromRoute]int ownerId)
    {
        return Ok(await _parkingHistoryService.GetParkingHistoryByOwnerIdAsync(ownerId));
    }
    
    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteParkingHistoryById([FromRoute]int id)
    {
       await _parkingHistoryService.DeleteParkingHistoryByIdAsync(id);
       return NoContent();
    }
}