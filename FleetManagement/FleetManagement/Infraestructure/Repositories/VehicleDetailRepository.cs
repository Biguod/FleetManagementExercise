using FleetManagement.Domain.Interfaces.Repositories;
using FleetManagement.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace FleetManagement.Infraestructure.Repositories
{
    public class VehicleDetailRepository : BaseRepository<VehicleDetail>, IVehicleDetailRepository
    {
        public VehicleDetailRepository(IServiceScopeFactory serviceScopeFactory) : base(serviceScopeFactory) { }

        public async Task<VehicleDetail> GetByVehicleType(VehicleTypeEnum vehicleType)
        {
            return await _databaseContext.Set<VehicleDetail>().Where(w => w.VehicleType == vehicleType).FirstOrDefaultAsync();
        }

        public async Task<bool> VerifyExistingVehicleDetail(Guid id, VehicleTypeEnum vehicleType)
        {
            return await _databaseContext.Set<VehicleDetail>().AnyAsync(a => a.Id != id && a.VehicleType == vehicleType);
        }
    }
}

