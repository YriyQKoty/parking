using Microsoft.AspNetCore.Mvc;
using Parking.Models;
using Parking.Services;

namespace Parking.Controllers;

public class ParkingDetailsController : BaseController
{
    private readonly IParkingDetailsService _parkingDetailsService;

    public ParkingDetailsController(IParkingDetailsService parkingDetailsService)
    {
        _parkingDetailsService = parkingDetailsService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllParkingDetails()
    {
        return Ok(await _parkingDetailsService.GetAllParkingDetailsAsync());
    }
    
    [HttpGet("id/{id}")]
    public async Task<IActionResult> GetAllParkingDetailsById([FromRoute] int id)
    {
        return Ok(await _parkingDetailsService.GetParkingDetailsByIdAsync(id));
    }
    
    [HttpGet("number")]
    public async Task<IActionResult> GetAllParkingDetailsByCarNumber([FromQuery] string number)
    {
        return Ok(await _parkingDetailsService.GetAllParkingDetailsByCarNumberAsync(number));
    }
    
    [HttpGet("carId/{carId}")]
    public async Task<IActionResult> GetAllParkingDetailsByCarId([FromRoute] int carId)
    {
        return Ok(await _parkingDetailsService.GetAllParkingDetailsByCarIdAsync(carId));
    }
    
    [HttpGet("phId/{parkingHistoryId}")]
    public async Task<IActionResult> GetAllParkingDetailsByParkingHistoryId([FromRoute] int parkingHistoryId)
    {
        return Ok(await _parkingDetailsService.GetAllParkingDetailsByParkingHistoryIdAsync(parkingHistoryId));
    }
    
    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteParkingDetailsById([FromRoute] int id)
    {
        await _parkingDetailsService.DeleteParkingDetailsById(id);
        return NoContent();
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateParkingDetails([FromBody] ParkingDetail parkingDetail)
    {
        return Ok(await _parkingDetailsService.CreateParkingDetailsAsync(parkingDetail));
    }
    
    [HttpPut]
    public async Task<IActionResult> UpdateParkingDetails([FromBody] ParkingDetail parkingDetail)
    {
        return Ok(await _parkingDetailsService.UpdateParkingDetailAsync(parkingDetail));
    }

}