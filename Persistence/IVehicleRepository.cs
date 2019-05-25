using System.Threading.Tasks;
using NetCore_Angular_Demo.Models;

namespace NetCore_Angular_Demo.Persistence
{
    public interface IVehicleRepository
    {
        Task<Vehicle> GetVehicle(int id);
    }
}