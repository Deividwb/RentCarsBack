
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using RentCars_Back.Models;
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

    [HttpGet("id")]
    public async Task<IActionResult> Get(int id)
    {
        var driver = await _repository.BuscaMotoristaAsync(id);

        return driver != null
         ? Ok(driver)
         : NotFound("Motorista não Encontrado");
    }

    [HttpPost]
    public async Task<IActionResult> Post(DriverRequest request)
    {
        var adicionado = await _repository.AdicionarAsync(request);
        return adicionado
                 ? Ok("Motorista Adicionado com Sucesso")
                 : BadRequest("Erro ao Adicionar Motorista");
    }

}
