using RentCars_Back.Models;
namespace RentCars_Back.Repository
{
    public interface IDriveRepository
    {
        Task<IEnumerable<DriveResponse>> BuscaMotoristasAsync();
        Task<DriveResponse> BuscaMotoristaAsync(int id);
        Task<bool> AdicionarAsync(DriveRequest request);
        Task<bool> AtualizarAsync(DriveRequest request, int id);
        Task<bool> DeletarAsync(int id);
    }
}