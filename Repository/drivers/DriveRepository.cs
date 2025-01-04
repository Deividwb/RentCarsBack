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
                     f.paymentDay AS PaymentDay,
                     f.location AS Location,
                     f.cep AS Cep,
                     f.city AS City,
                     f.district AS District,
                     f.uf AS Uf,
                     f.street AS Street,
                     f.number AS Number
                FROM drivers f
                WHERE f.id = @Id;";

            using var con = new NpgsqlConnection(connectionString);
            return await con.QueryFirstOrDefaultAsync<DriverResponse>(sql, new { Id = id });
        }

        public async Task<bool> AdicionarAsync(DriverRequest request)
        {
            string sql = @"INSERT INTO drivers (name, paymentDay, location, cep, city, district, uf, street, number)
                             VALUES (@Name, @PaymentDay, @Location, @Cep, @City, @District, @Uf, @Street, @Number)";

            using var con = new NpgsqlConnection(connectionString);
            return await con.ExecuteAsync(sql, request) > 0;
        }

        public async Task<bool> AtualizarAsync(DriverRequest request, int id)
        {
            string sql = @"UPDATE drivers SET 
                      name = @Name,
                      paymentDay = @PaymentDay,
                      location = @Location,
                      cep = @Cep,
                      city = @City,
                      district = @District,
                      uf = @Uf,
                      street = @Street,
                      number = @Number
                    WHERE id = @Id";

            var parametros = new DynamicParameters();
            parametros.Add("Name", request.Name);
            parametros.Add("PaymentDay", request.PaymentDay);
            parametros.Add("Location", request.Location);
            parametros.Add("Cep", request.Cep);
            parametros.Add("City", request.City);
            parametros.Add("District", request.District);
            parametros.Add("Uf", request.Uf);
            parametros.Add("Street", request.Street);
            parametros.Add("Number", request.Number);
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