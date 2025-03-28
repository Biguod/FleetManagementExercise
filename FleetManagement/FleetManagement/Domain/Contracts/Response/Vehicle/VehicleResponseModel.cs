using FleetManagement.Domain.Models;

namespace FleetManagement.Domain.Contracts.Response.Vehicle
{
    public class VehicleResponseModel
    {
        public string ChassisId { get; set; }
        public string ChassisSeries { get; set; }
        public int ChassisNumber { get; set; }
        public string VehicleType { get; set; }
        public int PassengersNumber { get; set; }
        public string Color { get; set; }
    }
}
