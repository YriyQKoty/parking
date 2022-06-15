using Microsoft.AspNetCore.Mvc;
using Parking.Models;
using Parking.Services;

namespace Parking.Controllers;

public class OwnersController : BaseController
{
    private readonly IOwnersService _ownersService;

    public OwnersController(IOwnersService ownersService)
    {
        _ownersService = ownersService;
    }

    [HttpGet("id/{id}")]
    public async Task<IActionResult> GetOwnerById([FromRoute] int id)
    {
        return Ok(await _ownersService.GetOwnerByIdAsync(id));
    }

    [HttpGet]
    public async Task<IActionResult> GetAllOwners()
    {
        return Ok(await _ownersService.GetAllOwnersAsync());
    }

    [HttpPost]
    public async Task<IActionResult> CreateOwner([FromBody]Owner owner)
    {
        return Ok(await _ownersService.CreateOwner(owner));
    }

    [HttpPut]
    public async Task<IActionResult> UpdateOwner([FromBody] Owner owner)
    {
        return Ok(await _ownersService.UpdateOwner(owner));
    }

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteOwnerById([FromRoute] int id)
    {
        await _ownersService.DeleteOwnerByIdAsync(id);
        return NoContent();
    }
}