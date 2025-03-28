using FleetManagement.Domain.Contracts.Request.VehicleDetail;
using FleetManagement.Domain.Contracts.Response.VehicleDetail;
using FleetManagement.Domain.Interfaces.Repositories;
using FleetManagement.Domain.Interfaces.Services;
using FleetManagement.Domain.Models;

namespace FleetManagement.Infraestructure.Services
{
    public class VehicleDetailService : IVehicleDetailService
    {
        private readonly IVehicleDetailRepository _vehicleDetailRepository;

        public VehicleDetailService(IVehicleDetailRepository vehicleDetailRepository)
        {
            _vehicleDetailRepository = vehicleDetailRepository;
        }

        public async Task<bool> DeleteVehicleDetail(Guid vehicleDetailId)
        {
            var vehicleDetail = await _vehicleDetailRepository.GetOne(vehicleDetailId);
            if (vehicleDetail == null)
                return false;
            await _vehicleDetailRepository.Delete(vehicleDetail);
            return true;
        }

        public async Task<GetAllVehicleDetailsResponseModel> GetAllActiveVehicleDetails()
        {
            var vehicleDetails = await _vehicleDetailRepository.GetAllActive();
            if (!vehicleDetails.Any())
                return null;

            var response = new GetAllVehicleDetailsResponseModel()
            {
                VehicleDetailList = vehicleDetails.Select(s => VehicleDetailToResponseModel(s)).ToList()
            };
            return response;
        }

        public async Task<GetAllVehicleDetailsResponseModel> GetAllVehicleDetails()
        {
            var vehicleDetails = await _vehicleDetailRepository.GetAll();
            if (!vehicleDetails.Any())
                return null;

            var response = new GetAllVehicleDetailsResponseModel()
            {
                VehicleDetailList = vehicleDetails.Select(s => VehicleDetailToResponseModel(s)).ToList()
            };
            return response;
        }

        public async Task<VehicleDetailResponseModel> GetVehicleDetailById(Guid request)
        {
            var response = await _vehicleDetailRepository.GetOne(request);
            if (response == null)
            {
                return null;
            }
            return VehicleDetailToResponseModel(response);
        }

        public async Task<VehicleDetail> GetVehicleDetailByVehicleType(VehicleTypeEnum vehicleType)
        {
            return await _vehicleDetailRepository.GetByVehicleType(vehicleType);
        }

        public async Task<Guid> InsertVehicleDetail(InsertVehicleDetailRequestModel request)
        {
            if (await _vehicleDetailRepository.Any(a => a.VehicleType == request.VehicleType)) throw new ArgumentException("This VehicleType already exists in the Database!");

            var vehicle = new VehicleDetail
            {
                VehicleType = request.VehicleType,
                PassengersNumber = request.PassengersNumber
            };
            return await _vehicleDetailRepository.Create(vehicle);
        }

        public async Task<bool> UpdateVehicleDetail(UpdateVehicleDetailRequestModel request)
        {
            var alreadyExists = await _vehicleDetailRepository.VerifyExistingVehicleDetail(request.Id, request.VehicleType);

            if (alreadyExists) throw new ArgumentException("This VehicleType already exists in the Database!");            

            var vehicleDetailDB = await _vehicleDetailRepository.GetOne(request.Id);
            if (vehicleDetailDB != null)
            {
                var vehicleDetail = new VehicleDetail
                {
                    Id = request.Id,
                    VehicleType = request.VehicleType,
                    PassengersNumber = (uint)request.PassengersNumber
                };
                _vehicleDetailRepository.Update(vehicleDetail);
                return await Task.FromResult(true);
            }
            return await Task.FromResult(false);
        }

        private VehicleDetailResponseModel VehicleDetailToResponseModel(VehicleDetail src)
        {
            return new VehicleDetailResponseModel()
            {
                Id = src.Id,
                VehicleType = src.VehicleType,
                Description = src.VehicleType.ToString(),
                PassengersNumber = (int)src.PassengersNumber
            };
        }
    }
}




