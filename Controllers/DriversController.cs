
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

    [HttpPut("id")]
    public async Task<IActionResult> Put(DriverRequest request, int id)
    {
        if (id <= 0) return BadRequest("Motorista Inválido");

        var driver = await _repository.BuscaMotoristaAsync(id);

        if (driver == null) return NotFound("Motorista não existe");

        if (string.IsNullOrEmpty(request.Name)) request.Name = driver.Name;
        if (request.Age <= 0) request.Age = driver.Age;
        if (string.IsNullOrEmpty(request.Address)) request.Address = driver.Address;
        if (string.IsNullOrEmpty(request.Sexo)) request.Sexo = driver.Sexo;
        if (string.IsNullOrEmpty(request.City)) request.City = driver.City;

        var atualizado = await _repository.AtualizarAsync(request, id);
        return atualizado
                 ? Ok("Motorista Atualizado com Sucesso")
                 : BadRequest("Erro ao atualizar Motorista");
    }

    [HttpDelete("id")]
    public async Task<IActionResult> Delete(int id)
    {
        if (id <= 0) return BadRequest("Motorista Inválido");

        var driver = await _repository.BuscaMotoristaAsync(id);

        if (driver == null) return NotFound("MOtorista não existe");


        var deletado = await _repository.DeletarAsync(id);
        return deletado
                 ? Ok("Motorista Deletado com Sucesso")
                 : BadRequest("Erro ao deletar Motorista");
    }

}
