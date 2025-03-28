using Moq;
using FleetManagement.Infraestructure.Services;
using FleetManagement.Domain.Interfaces.Repositories;
using FleetManagement.Domain.Interfaces.Services;
using FleetManagement.Domain.Models;
using FleetManagement.Domain.Contracts.Request.Vehicle;
using System.Linq.Expressions;


namespace FleetManagementTests.VehicleTests
{
    public class VehicleServiceTests
    {
        private readonly Mock<IVehicleRepository> _vehicleRepositoryMock;
        private readonly Mock<IVehicleDetailService> _vehicleDetailServiceMock;
        private readonly VehicleService _vehicleService;

        public VehicleServiceTests()
        {
            _vehicleRepositoryMock = new Mock<IVehicleRepository>();
            _vehicleDetailServiceMock = new Mock<IVehicleDetailService>();
            _vehicleService = new VehicleService(_vehicleRepositoryMock.Object, _vehicleDetailServiceMock.Object);
        }

        [Fact]
        public async Task DeleteVehicle_VehicleExists_ReturnsTrue()
        {
            var chassisSeries = "abc";
            var chassisNumber = 123;
            var chassisId = "abc123";
            var color = "Red";
            var vehicleDetailId = Guid.NewGuid();
            var vehicle = new Vehicle { ChassisSeries = chassisSeries, ChassisNumber = (uint)chassisNumber, VehicleDetailId = vehicleDetailId, Color = color };
            _vehicleRepositoryMock.Setup(x => x.GetVehicleByChassisId(chassisId)).ReturnsAsync(vehicle);
            _vehicleRepositoryMock.Setup(x => x.Delete(vehicle)).Returns(Task.CompletedTask);

            var result = await _vehicleService.DeleteVehicle(chassisId);

            Assert.True(result);
            _vehicleRepositoryMock.Verify(x => x.Delete(vehicle), Times.Once);
        }

        [Fact]
        public async Task DeleteVehicle_VehicleDoesNotExist_ReturnsFalse()
        {
            var chassisId = "123";
            _vehicleRepositoryMock.Setup(x => x.GetVehicleByChassisId(chassisId)).ReturnsAsync((Vehicle)null);

            var result = await _vehicleService.DeleteVehicle(chassisId);

            Assert.False(result);
        }

        [Fact]
        public async Task GetAllActiveVehicles_VehiclesExist_ReturnsVehicles()
        {
            var vehicles = new List<Vehicle>
        {
            new Vehicle { ChassisSeries = "A123", ChassisNumber = 12345, Color = "Red", VehicleDetailId = Guid.NewGuid(), Archived = false, Details = new VehicleDetail(){ VehicleType = VehicleTypeEnum.Truck, PassengersNumber = 1 }},
            new Vehicle { ChassisSeries = "A123", ChassisNumber = 12345, Color = "blue", VehicleDetailId = Guid.NewGuid(), Archived = false, Details = new VehicleDetail(){ VehicleType = VehicleTypeEnum.Truck, PassengersNumber = 1 }},
            new Vehicle { ChassisSeries = "A123", ChassisNumber = 12345, Color = "green", VehicleDetailId = Guid.NewGuid(), Archived = false, Details = new VehicleDetail(){ VehicleType = VehicleTypeEnum.Truck, PassengersNumber = 1 }}
        };
            _vehicleRepositoryMock.Setup(x => x.GetAllActive()).ReturnsAsync(vehicles);

            var result = await _vehicleService.GetAllActiveVehicles();

            Assert.NotNull(result);
            Assert.Equal(3, result.Vehicles.Count());
        }

        [Fact]
        public async Task GetAllActiveVehicles_NoVehicles_ReturnsNull()
        {
            _vehicleRepositoryMock.Setup(x => x.GetAllActive()).ReturnsAsync(new List<Vehicle>());

            var result = await _vehicleService.GetAllActiveVehicles();

            Assert.Null(result);
        }

        [Fact]
        public async Task GetVehicleByChassisId_VehicleExists_ReturnsVehicle()
        {
            var chassisId = "A12312345";
            var vehicle = new Vehicle { ChassisSeries = "A123", ChassisNumber = 12345, Color = "Red", VehicleDetailId = Guid.NewGuid(), Details = new VehicleDetail() { VehicleType = VehicleTypeEnum.Bus, PassengersNumber = 42 } };
            _vehicleRepositoryMock.Setup(x => x.GetVehicleByChassisId(chassisId)).ReturnsAsync(vehicle);

            var result = await _vehicleService.GetVehicleByChassisId(chassisId);

            Assert.NotNull(result);
            Assert.Equal(chassisId, result.ChassisId);
        }

        [Fact]
        public async Task GetVehicleByChassisId_VehicleDoesNotExist_ReturnsNull()
        {
            var chassisId = "123";
            _vehicleRepositoryMock.Setup(x => x.GetVehicleByChassisId(chassisId)).ReturnsAsync((Vehicle)null);

            var result = await _vehicleService.GetVehicleByChassisId(chassisId);

            Assert.Null(result);
        }

        [Fact]
        public async Task InsertVehicle_VehicleDetailDoesNotExist_ThrowsArgumentException()
        {
            var request = new InsertVehicleRequestModel
            {
                ChassisSeries = "A123",
                ChassisNumber = 12345,
                VehicleType = VehicleTypeEnum.Bus,
                Color = "Red"
            };
            _vehicleDetailServiceMock.Setup(x => x.GetVehicleDetailByVehicleType(request.VehicleType)).ReturnsAsync((VehicleDetail)null);

            await Assert.ThrowsAsync<ArgumentException>(() => _vehicleService.InsertVehicle(request));
        }

        [Fact]
        public async Task InsertVehicle_VehicleAlreadyExists_ThrowsArgumentException()
        {
            var request = new InsertVehicleRequestModel
            {
                ChassisSeries = "A123",
                ChassisNumber = 12345,
                VehicleType = VehicleTypeEnum.Car,
                Color = "Red"
            };
            _vehicleRepositoryMock.Setup(x => x.Any(It.IsAny<Expression<Func<Vehicle, bool>>>())).ReturnsAsync(true);

            await Assert.ThrowsAsync<ArgumentException>(() => _vehicleService.InsertVehicle(request));
        }

        [Fact]
        public async Task InsertVehicle_Success_ReturnsVehicleCreatedResponse()
        {
            var request = new InsertVehicleRequestModel
            {
                ChassisSeries = "A123",
                ChassisNumber = 12345,
                VehicleType = VehicleTypeEnum.Bus,
                Color = "Red"
            };

            var vehicleDetail = new VehicleDetail { Id = Guid.NewGuid(), VehicleType = VehicleTypeEnum.Bus };
            _vehicleDetailServiceMock.Setup(x => x.GetVehicleDetailByVehicleType(request.VehicleType)).ReturnsAsync(vehicleDetail);

            var vehicleGuid = Guid.NewGuid();
            var vehicle = new Vehicle
            {
                ChassisSeries = "A123",
                ChassisNumber = 12345,
                VehicleDetailId = vehicleDetail.Id,
                Color = "Red"
            };
            _vehicleRepositoryMock.Setup(x => x.Create(It.IsAny<Vehicle>())).ReturnsAsync(vehicleGuid);
            _vehicleRepositoryMock.Setup(x => x.GetOne(vehicleGuid)).ReturnsAsync(vehicle);

            var result = await _vehicleService.InsertVehicle(request);

            Assert.NotNull(result);
            Assert.Equal("Vehicle Created Successfully!", result.Message);
        }

        [Fact]
        public async Task UpdateVehicle_VehicleExists_UpdatesVehicle()
        {
            var request = new UpdateVehicleRequestModel { ChassisId = "a123", Color = "Blue" };
            var vehicle = new Vehicle { ChassisSeries = "a", ChassisNumber = 123, Color = "Red", VehicleDetailId = Guid.NewGuid() };
            _vehicleRepositoryMock.Setup(x => x.GetVehicleByChassisId(request.ChassisId)).ReturnsAsync(vehicle);
            _vehicleRepositoryMock.Setup(x => x.Update(It.IsAny<Vehicle>())).Returns(Task.CompletedTask);

            var result = await _vehicleService.UpdateVehicle(request);

            Assert.True(result);
            Assert.Equal("Blue", vehicle.Color);
        }

        [Fact]
        public async Task UpdateVehicle_VehicleDoesNotExist_ReturnsFalse()
        {
            var request = new UpdateVehicleRequestModel { ChassisId = "123", Color = "Blue" };
            _vehicleRepositoryMock.Setup(x => x.GetVehicleByChassisId(request.ChassisId)).ReturnsAsync((Vehicle)null);

            var result = await _vehicleService.UpdateVehicle(request);

            Assert.False(result);
        }
    }
}