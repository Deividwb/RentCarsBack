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
        public async Task<IEnumerable<DriveResponse>> BuscaMotoristasAsync()
        {
            string sql = @"SELECT *from drivers";
            using (var con = new NpgsqlConnection(connectionString))
            {
                return await con.QueryAsync<DriveResponse>(sql);

            }
        }

        public Task<DriveResponse> BuscaMotoristaAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AdicionarAsync(DriveRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AtualizarAsync(DriveRequest request, int id)
        {
            throw new NotImplementedException();
        }




        public Task<bool> DeletarAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}