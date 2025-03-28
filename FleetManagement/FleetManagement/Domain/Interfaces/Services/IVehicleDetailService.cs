using FleetManagement.Domain.Contracts.Request.Vehicle;
using FleetManagement.Domain.Contracts.Request.VehicleDetail;
using FleetManagement.Domain.Contracts.Response.VehicleDetail;
using FleetManagement.Domain.Models;

namespace FleetManagement.Domain.Interfaces.Services
{
    public interface IVehicleDetailService
    {
        Task<GetAllVehicleDetailsResponseModel> GetAllVehicleDetails();

        Task<GetAllVehicleDetailsResponseModel> GetAllActiveVehicleDetails();

        Task<Guid> InsertVehicleDetail(InsertVehicleDetailRequestModel request);

        Task<VehicleDetailResponseModel> GetVehicleDetailById(Guid request);

        Task<bool> UpdateVehicleDetail(UpdateVehicleDetailRequestModel request);

        Task<bool> DeleteVehicleDetail(Guid vehicleId);

        Task<VehicleDetail> GetVehicleDetailByVehicleType(VehicleTypeEnum vehicleType);
    }
}
