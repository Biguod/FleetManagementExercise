using FleetManagement.Domain.Models;

namespace FleetManagement.Domain.Interfaces.Repositories
{
    public interface IVehicleRepository : IBaseRepository<Vehicle>
    {
        Task<Vehicle> GetVehicleByChassisId(string chassisId);
    }
}
