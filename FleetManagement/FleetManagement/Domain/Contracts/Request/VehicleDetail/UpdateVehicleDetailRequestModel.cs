using FleetManagement.Domain.Models;

namespace FleetManagement.Domain.Contracts.Request.VehicleDetail
{
    public class UpdateVehicleDetailRequestModel
    {
        public Guid Id { get; set; }
        public VehicleTypeEnum VehicleType { get; set; }
        public int PassengersNumber { get; set; }
    }
}
