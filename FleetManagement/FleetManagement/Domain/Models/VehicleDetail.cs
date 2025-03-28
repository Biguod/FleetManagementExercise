using FleetManagement.Domain.Models.Common;

namespace FleetManagement.Domain.Models
{
    public class VehicleDetail : BaseClass
    {
        public VehicleTypeEnum VehicleType { get; set; }
        public uint PassengersNumber { get; set; }
    }
}
