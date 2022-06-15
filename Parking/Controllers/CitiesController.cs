using Microsoft.AspNetCore.Mvc;
using Parking.Models;
using Parking.Services;

namespace Parking.Controllers;

public class CitiesController : BaseController
{
    private readonly ICitiesService _citiesService;


    public CitiesController(ICitiesService citiesService)
    {
        _citiesService = citiesService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCities()
    {
        return Ok(await _citiesService.GetAllCitiesAsync());
    }
    
    [HttpGet("id/{id}")]
    public async Task<IActionResult> GetCityById([FromRoute]int id)
    {
        return Ok(await _citiesService.GetCityByIdAsync(id));
    }
    
    [HttpGet("name")]
    public async Task<IActionResult> GetCityByName([FromQuery]string name)
    {
        return Ok(await _citiesService.GetCityByNameAsync(name));
    }

    [HttpPost]
    public async Task<IActionResult> CreateCity([FromBody] City city)
    {
        return Ok(await _citiesService.CreateCity(city));
    }

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteCityById([FromRoute]int id)
    {
        await _citiesService.DeleteCityById(id);

        return NoContent();
    }
}