using Dapper;
using Npgsql;
using RentCars_Back.Models;

namespace RentCars_Back.Repository
{
    public class CarRepository : ICarRepository
    {
        private readonly IConfiguration _configuration;
        private readonly string connectionString;

        public CarRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            connectionString = _configuration.GetConnectionString("SqlConnection");
        }
        public async Task<IEnumerable<CarResponse>> BuscaCarrosAsync()
        {
            string sql = @"
      SELECT 
                c.Id,
                c.Brand,
                c.Plate,
                c.ModelYear,
                c.MonthLicensing,
                c.NumberRenavam,
                c.ColorCar,
                c.Id_Driver,
                c.ImageUrls as Image,
                d.Name AS DriverName            
            FROM Cars c
            INNER JOIN Drivers d ON c.Id_Driver = d.Id";

            using var con = new NpgsqlConnection(connectionString);
            var cars = await con.QueryAsync<CarResponse>(sql);

            // Para cada carro, obtenha as URLs das imagens associadas
            // foreach (var car in cars)
            // {
            //     car.ImageUrls = GetImageUrlsForCar(car.Id);

            //     //tratar a imagem
            //     // var cleanJson = car.ImageUrls.ToString().Trim('{', '}'); // Remove as chaves se existirem
            //     //     // Deserializa a string para uma lista de URLs
            //     //     car.ImageUrls = JsonConvert.DeserializeObject<List<string>>(cleanJson);
            //     //     using Newtonsoft.Json;
            // }

            return cars;
        }

        // Adicione a função para obter as URLs das imagens
        private List<string> GetImageUrlsForCar(int carId)
        {
            // Implemente a lógica para obter as URLs das imagens com base no carId
            // Você pode seguir o exemplo anterior para buscar as URLs com base nos nomes dos arquivos,
            // ou adaptá-lo de acordo com o seu sistema de armazenamento de imagens.

            // Exemplo:
            string imageDirectory = "C:\\Path\\To\\ImageDirectory"; // Substitua pelo seu caminho real
            List<string> imageUrls = new List<string>();

            if (Directory.Exists(imageDirectory))
            {
                string[] imageFiles = Directory.GetFiles(imageDirectory, $"{carId}.*");

                foreach (string imageFile in imageFiles)
                {
                    // Construa a URL da imagem com base no nome do arquivo
                    string imageUrl = $"/images/{Path.GetFileName(imageFile)}"; // Substitua pela rota real
                    imageUrls.Add(imageUrl);
                }
            }

            return imageUrls;
        }



        public async Task<CarResponse> BuscaCarroAsync(int id)
        {
            string sql = @"SELECT
                     f.id AS Id,
                     f.brand AS Brand,
                     f.plate AS Plate,
                     f.modelYear AS ModelYear,
                     f.monthLicensing AS MonthLicensing,
                     f.numberRenavam AS NumberRenavam,
                     f.colorCar AS ColorCar,
                     f.imageUrls As Image,
                     f.id_driver AS Id_Driver
                FROM cars f
                WHERE f.id = @Id;";

            using var con = new NpgsqlConnection(connectionString);
            return await con.QueryFirstOrDefaultAsync<CarResponse>(sql, new { Id = id });
        }

        public async Task<bool> AdicionarAsync(CarRequest request)
        {
            string sql = @"INSERT INTO cars (brand, plate, modelYear, monthLicensing, numberRenavam, colorCar, imageUrls, id_Driver)
                             VALUES (@Brand, @Plate, @ModelYear, @MonthLicensing, @NumberRenavam, @ColorCar, @ImageUrls, @Id_Driver)";

            using var con = new NpgsqlConnection(connectionString);
            return await con.ExecuteAsync(sql, request) > 0;
        }

        public async Task<bool> AtualizarAsync(CarRequest request, int id)
        {
            string sql = @"UPDATE cars SET 
                      brand = @Brand,
                      plate = @Plate,
                      modelYear = @ModelYear,
                      monthLicensing = @MonthLicensing,
                      numberRenavam = @NumberRenavam,
                      colorCar = @ColorCar,                 
                      id_driver = @Id_Driver
                    WHERE id = @Id";

            var parametros = new DynamicParameters();
            parametros.Add("Brand", request.Brand);
            parametros.Add("Plate", request.Plate);
            parametros.Add("ModelYear", request.ModelYear);
            parametros.Add("MonthLicensing", request.MonthLicensing);
            parametros.Add("NumberRenavam", request.NumberRenavam);
            parametros.Add("ColorCar", request.ColorCar);
            parametros.Add("Id_Driver", request.Id_Driver);
            parametros.Add("Id", id);


            using var con = new NpgsqlConnection(connectionString);
            return await con.ExecuteAsync(sql, parametros) > 0;
        }




        public async Task<bool> DeletarAsync(int id)
        {
            string sql = @"DELETE FROM cars                    
                              WHERE id = @Id";

            using var con = new NpgsqlConnection(connectionString);
            return await con.ExecuteAsync(sql, new { Id = id }) > 0;
        }
    }
}
