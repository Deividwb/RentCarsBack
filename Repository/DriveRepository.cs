using Dapper;
using Npgsql;
using RentCars_Back.Models;

namespace RentCars_Back.Repository
{
    public class DriveRepository : IDriveRepository
    {
        private readonly IConfiguration _configuration;
        private readonly string connectionString;

        public DriveRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            connectionString = _configuration.GetConnectionString("SqlConnection");
        }
        public async Task<IEnumerable<DriverResponse>> BuscaMotoristasAsync()
        {
            string sql = @"SELECT *from drivers";

            using var con = new NpgsqlConnection(connectionString);
            return await con.QueryAsync<DriverResponse>(sql);

        }

        public async Task<DriverResponse> BuscaMotoristaAsync(int id)
        {
            string sql = @"SELECT
                     f.id AS Id,
                     f.name AS Name,
                     f.age AS Age,
                     f.address AS Address,
                     f.sexo AS Sexo,
                     f.city AS City
                FROM drivers f
                WHERE f.id = @Id;";

            using var con = new NpgsqlConnection(connectionString);
            return await con.QueryFirstOrDefaultAsync<DriverResponse>(sql, new { Id = id });
        }

        public async Task<bool> AdicionarAsync(DriverRequest request)
        {
            string sql = @"INSERT INTO drivers (name, age, address, sexo, city)
                             VALUES (@Name, @Age, @Address, @sexo, @City)";

            using var con = new NpgsqlConnection(connectionString);
            return await con.ExecuteAsync(sql, request) > 0;
        }

        public async Task<bool> AtualizarAsync(DriverRequest request, int id)
        {
            string sql = @"UPDATE drivers SET 
                      name = @Name,
                      age = @Age,
                      address = @Address,
                      sexo = @Sexo,
                      city = @City
                    WHERE id = @Id";

            var parametros = new DynamicParameters();
            parametros.Add("Name", request.Name);
            parametros.Add("Age", request.Age);
            parametros.Add("Address", request.Address);
            parametros.Add("Sexo", request.Sexo);
            parametros.Add("City", request.City);
            parametros.Add("Id", id);


            using var con = new NpgsqlConnection(connectionString);
            return await con.ExecuteAsync(sql, parametros) > 0;
        }




        public async Task<bool> DeletarAsync(int id)
        {
            string sql = @"DELETE FROM drivers                    
                              WHERE id = @Id";

            using var con = new NpgsqlConnection(connectionString);
            return await con.ExecuteAsync(sql, new { Id = id }) > 0;
        }
    }
}