using Microsoft.AspNetCore.Mvc;
using Parking.Models;
using Parking.Services;

namespace Parking.Controllers;

public class StationsController : BaseController
{
    private readonly IStationsService _stationsService;

    public StationsController(IStationsService stationsService)
    {
        _stationsService = stationsService;
    }

    [HttpGet("id/{id}")]
    public async Task<IActionResult> GetStationById([FromRoute] int id)
    {
        return Ok(await _stationsService.GetStationByIdAsync(id));
    }
    
    [HttpGet("name")]
    public async Task<IActionResult> GetStationByName([FromQuery] string name)
    {
        return Ok(await _stationsService.GetStationByNameAsync(name));
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllStations()
    {
        return Ok(await _stationsService.GetAllStationsAsync());
    }
    
    [HttpGet("city/{cityName}")]
    public async Task<IActionResult> GetAllStationsInCity([FromQuery] string cityName)
    {
        return Ok(await _stationsService.GetAllStationsInCityAsync(cityName));
    }
    
    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteStationById([FromRoute] int id)
    {
        await _stationsService.DeleteStationById(id);

        return NoContent();
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateStation([FromBody] Station station)
    {
        return Ok(await _stationsService.CreateStationAsync(station));
    }
    
    [HttpPut]
    public async Task<IActionResult> UpdateStation([FromBody] Station station)
    {
        return Ok(await _stationsService.UpdateStationAsync(station));
    }
}