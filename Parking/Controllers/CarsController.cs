using Microsoft.AspNetCore.Mvc;
using Parking.Db;
using Parking.Models;
using Parking.Services;

namespace Parking.Controllers;

public class CarsController : BaseController
{
    private readonly ICarsService _carsService;

    public CarsController(ICarsService carsService)
    {
        _carsService = carsService;
    }

    [HttpGet("id/{id}")]
    public async Task<IActionResult> GetCarById([FromRoute]int id)
    {
        return Ok(await _carsService.GetCarByIdAsync(id));
    }
    
    [HttpGet("number")]
    public async Task<IActionResult> GetCarByNumber([FromQuery]string number)
    {
        return Ok(await _carsService.GetCarByNumberAsync(number));
    }
    
    [HttpGet("owner/{ownerId}")]
    public async Task<IActionResult> GetAllCarsByOwnerId([FromRoute]int ownerId)
    {
        return Ok(await _carsService.GetAllCarsByOwnerIdAsync(ownerId));
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllCars()
    {
        return Ok(await _carsService.GetAllCarsAsync());
    }
    
    [HttpPut("update/status_{id}")]
    public async Task<IActionResult> ChangeParkingStatus([FromRoute]int id,[FromBody] bool status)
    {
        return Ok(await _carsService.ChangeParkingStatus(id, status));
    }
    
    [HttpPut("update")]
    public async Task<IActionResult> UpdateCar([FromBody] Car car)
    {
        return Ok(await _carsService.UpdateCarAsync(car));
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateCar([FromBody] Car car)
    {
        return Ok(await _carsService.CreateCarAsync(car));
    }

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteCarById([FromRoute]int id)
    {
        await _carsService.DeleteCarByIdAsync(id);
        return NoContent();
    }
    
    [HttpDelete]
    public async Task<IActionResult> DeleteCarByNumber([FromQuery]string number)
    {
        await _carsService.DeleteCarByNumberAsync(number);
        return NoContent();
    }
}