using FleetManagement.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace FleetManagement.Infraestructure.Context
{
    public class FleetManagementDbContext : DbContext
    {
        public FleetManagementDbContext(DbContextOptions<FleetManagementDbContext> options) : base(options) { }

        public DbSet<Vehicle> Vehicle { get; set; }
        public DbSet<VehicleDetail> VehicleDetail { get; set; }
    }
}
