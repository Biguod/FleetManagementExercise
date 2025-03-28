using FleetManagement.Domain.Models;

namespace FleetManagement.Domain.Contracts.Request.Vehicle
{
    public class UpdateVehicleRequestModel
    {
        public string ChassisId { get; set; }
        public string Color { get; set; }
    }
}
