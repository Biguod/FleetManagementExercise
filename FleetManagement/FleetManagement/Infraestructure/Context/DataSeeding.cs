using FleetManagement.Domain.Models;

namespace FleetManagement.Infraestructure.Context
{
    public class DataSeeding
    {
        private FleetManagementDbContext _db;

        public DataSeeding(FleetManagementDbContext db)
        {
            _db = db;
        }

        public void SeedData()
        {
            _db.VehicleDetail.AddRange(GetTestData());
            _db.SaveChanges();
        }

        private List<VehicleDetail> GetTestData()
        {
            return new List<VehicleDetail>()
            {
                new VehicleDetail { Id = Guid.NewGuid(), VehicleType = VehicleTypeEnum.Bus, PassengersNumber = 42, Archived = false, CreateDate = DateTime.Now, LastUpdate = DateTime.Now},
                new VehicleDetail { Id = Guid.NewGuid(), VehicleType = VehicleTypeEnum.Truck, PassengersNumber = 1, Archived = false, CreateDate = DateTime.Now, LastUpdate = DateTime.Now },
                new VehicleDetail { Id = Guid.NewGuid(), VehicleType = VehicleTypeEnum.Car, PassengersNumber = 4, Archived = false, CreateDate = DateTime.Now, LastUpdate = DateTime.Now }
            };
        }
    }
}
