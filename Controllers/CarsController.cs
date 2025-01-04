
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using RentCars_Back.Models;
using RentCars_Back.Repository;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using System.Net;


namespace RentCars_Back.Controllers;
[ApiController]
[Route("api/[controller]")]

public class CarsController : ControllerBase
{
    private readonly ICarRepository _repository;
    private readonly IDriveRepository _repository_driver;

    public CarsController(ICarRepository repository, IDriveRepository repositoryDriver)
    {
        _repository = repository;
        _repository_driver = repositoryDriver;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var cars = await _repository.BuscaCarrosAsync();
        var drivers = await _repository_driver.BuscaMotoristasAsync();

        // Combine cars with their respective drivers
        var carsWithImages = new List<CarResponse>();
        foreach (var car in cars)
        {
            var driver = drivers.FirstOrDefault(d => d.Id == car.Id_Driver);
            if (driver != null)
            {
                var imageUrls = GetImageUrlsForCar(car.Id); //tenho imagens no banco
                //porque que no get nao vem nada
                carsWithImages.Add(new CarResponse
                {
                    Id = car.Id,
                    Brand = car.Brand,
                    Plate = car.Plate,
                    ModelYear = car.ModelYear,
                    MonthLicensing = car.MonthLicensing,
                    NumberRenavam = car.NumberRenavam,
                    ColorCar = car.ColorCar,
                    Id_Driver = car.Id_Driver,
                    DriverName = driver.Name,
                    ImageUrls = car.ImageUrls
                });
            }
        }

        return carsWithImages.Any() ? Ok(carsWithImages) : NoContent();
    }


    // Método privado para obter as URLs das imagens
    private List<string> GetImageUrlsForCar(int carId)
    {
        // Implemente a lógica para obter as URLs das imagens com base no carId
        // Esta função é definida no seu repositório CarRepository.
        // Substitua com a lógica real de obtenção das URLs das imagens.

        // Exemplo:
        return new List<string>
        {
         "https://thumbs.dreamstime.com/z/close-up-do-dinossauro-22779278.jpg?ct=jpeg",
         "https://res.cloudinary.com/dcbanryd0/image/upload/v1735618157/hwax5yd6mkgy3zgkdb8w.jpg",
         "https://start.youse.com.br/wp-content/uploads/2020/08/chiron_record_0501-e1579719434722.jpg",
         "https://www.otempo.com.br/image/contentid/policy:1.2890008:1692745133/Bugatti-Centodieci-2020-1280-13-jpg.jpg?f=3x2&w=1224",
         "https://img.freepik.com/fotos-gratis/pessoa-andando-de-moto-poderosa_23-2150704855.jpg?size=626&ext=jpg&ga=GA1.1.386372595.1697760000&semt=sph",
         "https://blog.usezapay.com.br/wp-content/uploads/2022/11/kawasaki_ninja_zx-10r_krt_editio-scaled-1-610x610.webp",
         "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSCC5zhZ5JL4tTL0iOqoKKAYW95ykYEqC_Pkg86J4R-Vg&s",
         "https://i.postimg.cc/XYwQ7r9r/Whats-App-Image-2024-12-20-at-17-31-47-3.jpg",
         "https://res.cloudinary.com/dcbanryd0/image/upload/v1735618157/cld-sample-5.jpg"
        };
    }

    [HttpGet("id")]
    public async Task<IActionResult> Get(int id)
    {
        var car = await _repository.BuscaCarroAsync(id);

        return car != null
         ? Ok(car)
         : NotFound("Carro não Encontrado");
    }

    [HttpPost]
    public async Task<IActionResult> Post(CarRequest request)
    //https://console.cloudinary.com/pm/c-608c88b9bdf34441af6ae7fcc19117/media-explorer/carros
    //estou usando para o upload das imagens
    {
        var cloudinary = new Cloudinary(new Account("dcbanryd0", "444753294961396", "F5TQohyn773mAfNa4iMZAasn_NI"));

        var imageUrls = new List<string>();
        // Para cada imagem no request, faça o upload para o Cloudinary
        foreach (var imagePath in request.ImageUrls)
        {
            // Prepare os parâmetros de upload para o Cloudinary
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(imagePath), // Caminho do arquivo de imagem (isso pode ser uma URL de imagem ou caminho local)
                Folder = "carros" // Define a pasta onde a imagem será armazenada no Cloudinary
            };

            // Execute o upload da imagem
            var result = cloudinary.Upload(uploadParams);

            // Verifique se o upload foi bem-sucedido e adicione a URL da imagem à lista
            if (result?.StatusCode == HttpStatusCode.OK)
            {
                // Adiciona a URL da imagem ao resultado
                imageUrls.Add(result.SecureUrl.ToString());
            }
            else
            {
                // Em caso de erro, retorne BadRequest
                return BadRequest($"Erro ao fazer o upload da imagem: {imagePath}");
            }
        }

        // Atribua as URLs das imagens ao objeto request
        request.ImageUrls = imageUrls;

        // Adicione o carro ao banco de dados
        var adicionado = await _repository.AdicionarAsync(request);

        // Retorne a resposta dependendo do resultado
        return adicionado
            ? Ok("Carro Adicionado com Sucesso")
            : BadRequest("Erro ao Adicionar Carro");
    }

    [HttpPut("id")]
    public async Task<IActionResult> Put(CarRequest request, int id)
    {
        if (id <= 0) return BadRequest("Carro Inválido");

        var car = await _repository.BuscaCarroAsync(id);

        if (car == null) return NotFound("Carro não existe");

        if (string.IsNullOrEmpty(request.Brand)) request.Brand = car.Brand;
        if (string.IsNullOrEmpty(request.Plate)) request.Plate = car.Plate;
        if (request.ModelYear <= 0) request.ModelYear = car.ModelYear;
        if (string.IsNullOrEmpty(request.MonthLicensing)) request.MonthLicensing = car.MonthLicensing;
        if (request.NumberRenavam <= 0) request.NumberRenavam = car.NumberRenavam;
        if (string.IsNullOrEmpty(request.ColorCar)) request.ColorCar = car.ColorCar;
        if (request.Id_Driver <= 0) request.Id_Driver = car.Id_Driver;

        var atualizado = await _repository.AtualizarAsync(request, id);
        return atualizado
                 ? Ok("Carro Atualizado com Sucesso")
                 : BadRequest("Erro ao atualizar Carro");
    }

    [HttpDelete("id")]
    public async Task<IActionResult> Delete(int id)
    {
        if (id <= 0) return BadRequest("Carro Inválido");

        var car = await _repository.BuscaCarroAsync(id);

        if (car == null) return NotFound("Carro não existe");


        var deletado = await _repository.DeletarAsync(id);
        return deletado
                 ? Ok("Carro Deletado com Sucesso")
                 : BadRequest("Erro ao deletar Carro");
    }

}
