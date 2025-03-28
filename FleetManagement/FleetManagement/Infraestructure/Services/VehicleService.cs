using FleetManagement.Domain.Contracts.Request.Vehicle;
using FleetManagement.Domain.Contracts.Response.Vehicle;
using FleetManagement.Domain.Contracts.Response.VehicleDetail;
using FleetManagement.Domain.Interfaces.Repositories;
using FleetManagement.Domain.Interfaces.Services;
using FleetManagement.Domain.Models;

namespace FleetManagement.Infraestructure.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IVehicleDetailService _vehicleDetailService;

        public VehicleService(IVehicleRepository vehicleRepository, IVehicleDetailService vehicleDetailService)
        {
            _vehicleRepository = vehicleRepository;
            _vehicleDetailService = vehicleDetailService;
        }

        public async Task<bool> DeleteVehicle(string chassisId)
        {

            var vehicle = await _vehicleRepository.GetVehicleByChassisId(chassisId);
            if (vehicle == null)
                return false;
            await _vehicleRepository.Delete(vehicle);
            return true;

        }

        public async Task<GetAllVehiclesResponseModel> GetAllActiveVehicles()
        {
            var vehicles = await _vehicleRepository.GetAllActive();
            if (!vehicles.Any())
                return null;
            var response = new GetAllVehiclesResponseModel()
            {
                Vehicles = vehicles.Select(s => VehicleToResponseModel(s)).ToList()
            };
            return response;
        }

        public async Task<GetAllVehiclesResponseModel> GetAllVehicles()
        {
            var vehicles = await _vehicleRepository.GetAll();
            if (!vehicles.Any())
                return null;
            var response = new GetAllVehiclesResponseModel()
            {
                Vehicles = vehicles.Select(s => VehicleToResponseModel(s)).ToList()
            };
            return response;
        }

        public async Task<VehicleResponseModel> GetVehicleByChassisId(string chassisId)
        {
            var vehicle = await _vehicleRepository.GetVehicleByChassisId(chassisId);
            if (vehicle == null)
            {
                return null;
            }
            return VehicleToResponseModel(vehicle);
        }

        public async Task<InsertVehicleResponseModel> InsertVehicle(InsertVehicleRequestModel request)
        {
            if (await _vehicleRepository.Any(a => a.ChassisSeries == request.ChassisSeries && a.ChassisNumber == request.ChassisNumber)) throw new ArgumentException("ChassisId already exists!");
            var vehicleDetail = await _vehicleDetailService.GetVehicleDetailByVehicleType(request.VehicleType);
            if (vehicleDetail == null) throw new ArgumentException("VehicleDetail does not exist!");

            var vehicle = new Vehicle
            {
                ChassisSeries = request.ChassisSeries,
                ChassisNumber = request.ChassisNumber,
                VehicleDetailId = vehicleDetail.Id,
                Color = request.Color
            };

            var vehicleGuid = await _vehicleRepository.Create(vehicle);

            var vehicleCreated = await _vehicleRepository.GetOne(vehicleGuid);

            if (vehicleCreated != null)
            {
                return new InsertVehicleResponseModel()
                {
                    ChassisId = vehicleCreated.ChassisId,
                    Message = "Vehicle Created Successfully!"
                };
            }
            return new InsertVehicleResponseModel()
            {
                ChassisId = "",
                Message = "Vehicle Creation failed!"
            };
        }

        public async Task<bool> UpdateVehicle(UpdateVehicleRequestModel request)
        {
            var oldVehicle = await _vehicleRepository.GetVehicleByChassisId(request.ChassisId);
            if (oldVehicle != null)
            {
                oldVehicle.Color = request.Color;
                await _vehicleRepository.Update(oldVehicle);
                return await Task.FromResult(true);
            }
            return await Task.FromResult(false);
        }

        private VehicleResponseModel VehicleToResponseModel(Vehicle src)
        {
            return new VehicleResponseModel()
            {
                ChassisId = src.ChassisId,
                ChassisSeries = src.ChassisSeries,
                ChassisNumber = (int)src.ChassisNumber,
                VehicleType = src.Details.VehicleType.ToString(),
                PassengersNumber = (int)src.Details.PassengersNumber,
                Color = src.Color
            };
        }
    }
    
}
