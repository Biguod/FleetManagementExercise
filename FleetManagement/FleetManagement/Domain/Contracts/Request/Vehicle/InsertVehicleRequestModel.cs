using FleetManagement.Domain.Models;

namespace FleetManagement.Domain.Contracts.Request.Vehicle
{
    public class InsertVehicleRequestModel
    {
        public string ChassisSeries { get; set; }
        public uint ChassisNumber { get; set; }
        public VehicleTypeEnum VehicleType { get; set; }
        public string Color { get; set; }
    }
}
