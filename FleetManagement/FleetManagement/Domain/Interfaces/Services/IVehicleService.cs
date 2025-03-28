using FleetManagement.Domain.Contracts.Request.Vehicle;
using FleetManagement.Domain.Contracts.Response.Vehicle;
using FleetManagement.Domain.Models;

namespace FleetManagement.Domain.Interfaces.Services
{
    public interface IVehicleService
    {
        Task<GetAllVehiclesResponseModel> GetAllVehicles();
        Task<GetAllVehiclesResponseModel> GetAllActiveVehicles();

        Task<InsertVehicleResponseModel> InsertVehicle(InsertVehicleRequestModel request);

        Task<VehicleResponseModel> GetVehicleByChassisId(string request);

        Task<bool> UpdateVehicle(UpdateVehicleRequestModel request);

        Task<bool> DeleteVehicle(string chassisId);
    }
}
