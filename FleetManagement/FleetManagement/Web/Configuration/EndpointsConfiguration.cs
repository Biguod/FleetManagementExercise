using FleetManagement.API.Endpoints;
using FleetManagement.Web.Endpoints;

namespace FleetManagement.Web.Configuration
{
    public static class EndpointsConfiguration
    {
        public static void AddEndpoints(this WebApplication app)
        {
            VehicleEndpoints.AddVehicleEndpoints(app);
            VehicleDetailEndpoints.AddVehicleDetailEndpoints(app);
        }
    }
}
