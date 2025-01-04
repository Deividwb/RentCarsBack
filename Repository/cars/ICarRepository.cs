using RentCars_Back.Models;
namespace RentCars_Back.Repository
{
    public interface ICarRepository
    {
        Task<IEnumerable<CarResponse>> BuscaCarrosAsync();
        Task<CarResponse> BuscaCarroAsync(int id);
        Task<bool> AdicionarAsync(CarRequest request);
        Task<bool> AtualizarAsync(CarRequest request, int id);
        Task<bool> DeletarAsync(int id);
    }
}