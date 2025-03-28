using Moq;
using Xunit;
using FleetManagement.Infraestructure.Services;
using FleetManagement.Domain.Interfaces.Repositories;
using FleetManagement.Domain.Models;
using FleetManagement.Domain.Contracts.Request.VehicleDetail;
using FleetManagement.Domain.Contracts.Response.VehicleDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;


namespace FleetManagementTests.VehicleDetailTests
{
    public class VehicleDetailServiceTests
    {
        private readonly Mock<IVehicleDetailRepository> _vehicleDetailRepositoryMock;
        private readonly VehicleDetailService _vehicleDetailService;

        public VehicleDetailServiceTests()
        {
            _vehicleDetailRepositoryMock = new Mock<IVehicleDetailRepository>();
            _vehicleDetailService = new VehicleDetailService(_vehicleDetailRepositoryMock.Object);
        }

        [Fact]
        public async Task DeleteVehicleDetail_VehicleDetailExists_ReturnsTrue()
        {
            var vehicleDetailId = Guid.NewGuid();
            var vehicleDetail = new VehicleDetail { Id = vehicleDetailId };
            _vehicleDetailRepositoryMock.Setup(x => x.GetOne(vehicleDetailId)).ReturnsAsync(vehicleDetail);
            _vehicleDetailRepositoryMock.Setup(x => x.Delete(vehicleDetail)).Returns(Task.CompletedTask);

            var result = await _vehicleDetailService.DeleteVehicleDetail(vehicleDetailId);

            Assert.True(result);
            _vehicleDetailRepositoryMock.Verify(x => x.Delete(vehicleDetail), Times.Once);
        }

        [Fact]
        public async Task DeleteVehicleDetail_VehicleDetailDoesNotExist_ReturnsFalse()
        {
            var vehicleDetailId = Guid.NewGuid();
            _vehicleDetailRepositoryMock.Setup(x => x.GetOne(vehicleDetailId)).ReturnsAsync((VehicleDetail)null);

            var result = await _vehicleDetailService.DeleteVehicleDetail(vehicleDetailId);

            Assert.False(result);
        }

        [Fact]
        public async Task GetAllActiveVehicleDetails_VehicleDetailsExist_ReturnsVehicleDetails()
        {
            var vehicleDetails = new List<VehicleDetail>
        {
            new VehicleDetail { Id = Guid.NewGuid(), VehicleType = VehicleTypeEnum.Car, PassengersNumber = 4 }
        };
            _vehicleDetailRepositoryMock.Setup(x => x.GetAllActive()).ReturnsAsync(vehicleDetails);

            var result = await _vehicleDetailService.GetAllActiveVehicleDetails();

            Assert.NotNull(result);
            Assert.Single(result.VehicleDetailList);
        }

        [Fact]
        public async Task GetAllActiveVehicleDetails_NoVehicleDetails_ReturnsNull()
        {
            _vehicleDetailRepositoryMock.Setup(x => x.GetAllActive()).ReturnsAsync(new List<VehicleDetail>());

            var result = await _vehicleDetailService.GetAllActiveVehicleDetails();

            Assert.Null(result);
        }

        [Fact]
        public async Task GetVehicleDetailById_VehicleDetailExists_ReturnsVehicleDetail()
        {
            var vehicleDetailId = Guid.NewGuid();
            var vehicleDetail = new VehicleDetail { Id = vehicleDetailId, VehicleType = VehicleTypeEnum.Car, PassengersNumber = 4 };
            _vehicleDetailRepositoryMock.Setup(x => x.GetOne(vehicleDetailId)).ReturnsAsync(vehicleDetail);

            var result = await _vehicleDetailService.GetVehicleDetailById(vehicleDetailId);

            Assert.NotNull(result);
            Assert.Equal(vehicleDetailId, result.Id);
        }

        [Fact]
        public async Task GetVehicleDetailById_VehicleDetailDoesNotExist_ReturnsNull()
        {
            var vehicleDetailId = Guid.NewGuid();
            _vehicleDetailRepositoryMock.Setup(x => x.GetOne(vehicleDetailId)).ReturnsAsync((VehicleDetail)null);

            var result = await _vehicleDetailService.GetVehicleDetailById(vehicleDetailId);

            Assert.Null(result);
        }

        [Fact]
        public async Task GetVehicleDetailByVehicleType_VehicleDetailExists_ReturnsVehicleDetail()
        {
            var vehicleType = VehicleTypeEnum.Car;
            var vehicleDetail = new VehicleDetail { VehicleType = vehicleType, PassengersNumber = 4 };
            _vehicleDetailRepositoryMock.Setup(x => x.GetByVehicleType(vehicleType)).ReturnsAsync(vehicleDetail);

            var result = await _vehicleDetailService.GetVehicleDetailByVehicleType(vehicleType);

            Assert.NotNull(result);
            Assert.Equal(vehicleType, result.VehicleType);
        }

        [Fact]
        public async Task GetVehicleDetailByVehicleType_VehicleDetailDoesNotExist_ReturnsNull()
        {
            var vehicleType = VehicleTypeEnum.Car;
            _vehicleDetailRepositoryMock.Setup(x => x.GetByVehicleType(vehicleType)).ReturnsAsync((VehicleDetail)null);

            var result = await _vehicleDetailService.GetVehicleDetailByVehicleType(vehicleType);

            Assert.Null(result);
        }

        [Fact]
        public async Task InsertVehicleDetail_VehicleTypeAlreadyExists_ThrowsArgumentException()
        {
            var request = new InsertVehicleDetailRequestModel
            {
                VehicleType = VehicleTypeEnum.Car,
                PassengersNumber = 4
            };
            _vehicleDetailRepositoryMock.Setup(x => x.Any(It.IsAny<Expression<Func<VehicleDetail, bool>>>())).ReturnsAsync(true);

            await Assert.ThrowsAsync<ArgumentException>(() => _vehicleDetailService.InsertVehicleDetail(request));
        }

        [Fact]
        public async Task InsertVehicleDetail_Success_ReturnsGuid()
        {
            var request = new InsertVehicleDetailRequestModel
            {
                VehicleType = VehicleTypeEnum.Car,
                PassengersNumber = 4
            };
            _vehicleDetailRepositoryMock.Setup(x => x.Any(It.IsAny<Expression<Func<VehicleDetail, bool>>>())).ReturnsAsync(false);
            _vehicleDetailRepositoryMock.Setup(x => x.Create(It.IsAny<VehicleDetail>())).ReturnsAsync(Guid.NewGuid());

            var result = await _vehicleDetailService.InsertVehicleDetail(request);

            Assert.NotEqual(Guid.Empty, result);
        }

        [Fact]
        public async Task UpdateVehicleDetail_VehicleTypeAlreadyExists_ThrowsArgumentException()
        {
            var request = new UpdateVehicleDetailRequestModel
            {
                Id = Guid.NewGuid(),
                VehicleType = VehicleTypeEnum.Car,
                PassengersNumber = 4
            };
            _vehicleDetailRepositoryMock.Setup(x => x.VerifyExistingVehicleDetail(request.Id, request.VehicleType)).ReturnsAsync(true);

            await Assert.ThrowsAsync<ArgumentException>(() => _vehicleDetailService.UpdateVehicleDetail(request));
        }

        [Fact]
        public async Task UpdateVehicleDetail_VehicleDetailExists_UpdatesVehicleDetail()
        {
            var request = new UpdateVehicleDetailRequestModel
            {
                Id = Guid.NewGuid(),
                VehicleType = VehicleTypeEnum.Car,
                PassengersNumber = 4
            };
            var vehicleDetail = new VehicleDetail { Id = request.Id, VehicleType = request.VehicleType, PassengersNumber = 2 };
            _vehicleDetailRepositoryMock.Setup(x => x.VerifyExistingVehicleDetail(request.Id, request.VehicleType)).ReturnsAsync(false);
            _vehicleDetailRepositoryMock.Setup(x => x.GetOne(request.Id)).ReturnsAsync(vehicleDetail);
            _vehicleDetailRepositoryMock.Setup(x => x.Update(It.IsAny<VehicleDetail>())).Returns(Task.CompletedTask);

            var result = await _vehicleDetailService.UpdateVehicleDetail(request);

            Assert.True(result);
        }

        [Fact]
        public async Task UpdateVehicleDetail_VehicleDetailDoesNotExist_ReturnsFalse()
        {
            var request = new UpdateVehicleDetailRequestModel
            {
                Id = Guid.NewGuid(),
                VehicleType = VehicleTypeEnum.Car,
                PassengersNumber = 4
            };
            _vehicleDetailRepositoryMock.Setup(x => x.GetOne(request.Id)).ReturnsAsync((VehicleDetail)null);

            var result = await _vehicleDetailService.UpdateVehicleDetail(request);

            Assert.False(result);
        }
    }
}