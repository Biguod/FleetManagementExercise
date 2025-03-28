using System.Reflection;
using FleetManagement.Domain.Interfaces.Repositories;
using FleetManagement.Domain.Interfaces.Services;
using FleetManagement.Infraestructure.Repositories;
using FleetManagement.Infraestructure.Services;

namespace FleetManagement.Infraestructure.Extensions
{
    public static class DependencyInjectionConfig
    {
        public static void AddRepositoryDependency(this IServiceCollection services)
        {
            services.AddScoped<IVehicleDetailRepository, VehicleDetailRepository>();
            services.AddScoped<IVehicleRepository, VehicleRepository>();
        }

        public static void AddServiceDependency(this IServiceCollection services)
        {
            services.AddScoped<IVehicleDetailService, VehicleDetailService>();
            services.AddScoped<IVehicleService, VehicleService>();
        }
    }
}
