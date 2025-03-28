using FleetManagement.Domain.Models;

namespace FleetManagement.Domain.Interfaces.Repositories
{
    public interface IVehicleDetailRepository : IBaseRepository<VehicleDetail>
    {
        Task<bool> VerifyExistingVehicleDetail(Guid id, VehicleTypeEnum vehicleType);
        
        Task<VehicleDetail> GetByVehicleType(VehicleTypeEnum vehicleType);
    }
}
