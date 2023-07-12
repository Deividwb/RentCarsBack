using RentCars_Back.Models;
namespace RentCars_Back.Repository
{
    public interface IDriveRepository
    {
        Task<IEnumerable<DriverResponse>> BuscaMotoristasAsync();
        Task<DriverResponse> BuscaMotoristaAsync(int id);
        Task<bool> AdicionarAsync(DriverRequest request);
        Task<bool> AtualizarAsync(DriverRequest request, int id);
        Task<bool> DeletarAsync(int id);
    }
}