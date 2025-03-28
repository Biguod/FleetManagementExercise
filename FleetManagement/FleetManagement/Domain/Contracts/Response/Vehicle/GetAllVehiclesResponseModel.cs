using FleetManagement.Domain.Models;

namespace FleetManagement.Domain.Contracts.Response.Vehicle
{
    public class GetAllVehiclesResponseModel
    {
        public List<VehicleResponseModel> Vehicles { get; set; }
    }
}
