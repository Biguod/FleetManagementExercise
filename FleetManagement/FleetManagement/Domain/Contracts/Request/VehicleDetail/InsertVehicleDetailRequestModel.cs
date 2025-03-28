using FleetManagement.Domain.Models;

namespace FleetManagement.Domain.Contracts.Request.VehicleDetail
{
    public class InsertVehicleDetailRequestModel
    {
        public VehicleTypeEnum VehicleType { get; set; }
        public uint PassengersNumber { get; set; }
    }
}
