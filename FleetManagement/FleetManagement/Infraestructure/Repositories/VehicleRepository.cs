using System.Linq.Expressions;
using FleetManagement.Domain.Interfaces.Repositories;
using FleetManagement.Domain.Models;
using FleetManagement.Infraestructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FleetManagement.Infraestructure.Repositories
{
    public class VehicleRepository : BaseRepository<Vehicle>, IVehicleRepository
    {
        public VehicleRepository(IServiceScopeFactory serviceScopeFactory) : base(serviceScopeFactory) { }

        public async Task<Vehicle> GetVehicleByChassisId(string chassisId)
        {
            return await _databaseContext.Set<Vehicle>().Include(i => i.Details).Where(w => w.ChassisId == chassisId)
                //.Where(w => string.Format("{0}{1}", w.ChassisSeries, w.ChassisNumber) == chassisId)
                .FirstOrDefaultAsync();
        }
    }
}
