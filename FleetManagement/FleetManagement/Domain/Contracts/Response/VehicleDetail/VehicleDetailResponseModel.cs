using FleetManagement.Domain.Models;

namespace FleetManagement.Domain.Contracts.Response.VehicleDetail
{
    public class VehicleDetailResponseModel
    {
        public Guid Id { get; set; }
        public VehicleTypeEnum VehicleType { get; set; }
        public string Description { get; set; }
        public int PassengersNumber { get; set; }
    }
}
