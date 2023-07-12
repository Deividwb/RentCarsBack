
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using RentCars_Back.Repository;

namespace RentCars_Back.Controllers;
[ApiController]
[Route("api/[controller]")]

public class DriversController : ControllerBase
{
    private readonly IDriveRepository _repository;

    public DriversController(IDriveRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]

    public async Task<IActionResult> Get()
    {


        var drivers = await _repository.BuscaMotoristasAsync();

        return drivers.Any() ? Ok(drivers) : NoContent();
    }

}
