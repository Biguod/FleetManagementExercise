using FluentValidation;
using FleetManagement.Domain.Contracts.Request.VehicleDetail;
using FleetManagement.Web.Validators;
using FleetManagement.Domain.Constants;
using Xunit;
using FleetManagement.Domain.Models;

namespace FleetManagement.Tests.Validators
{
    public class InsertVehicleDetailRequestModelValidatorTests
    {
        private readonly InsertVehicleDetailRequestModelValidator _validator;

        public InsertVehicleDetailRequestModelValidatorTests()
        {
            _validator = new InsertVehicleDetailRequestModelValidator();
        }

        [Fact]
        public void Should_Have_Error_When_VehicleType_Is_Invalid()
        {
            var model = new InsertVehicleDetailRequestModel
            {
                VehicleType = (VehicleTypeEnum)999,
                PassengersNumber = 4
            };

            var validationResult = _validator.Validate(model);

            Assert.False(validationResult.IsValid);
            Assert.Contains(validationResult.Errors, e => e.PropertyName == "VehicleType" &&
                                                          e.ErrorMessage == ValidatorErrorMessages.VehicleType_InvalidEnum);
        }

        [Fact]
        public void Should_Have_Error_When_PassengerNumber_Is_Zero()
        {
            var model = new InsertVehicleDetailRequestModel
            {
                VehicleType = VehicleTypeEnum.Car,
                PassengersNumber = 0 
            };

            var validationResult = _validator.Validate(model);

            Assert.False(validationResult.IsValid);
            Assert.Contains(validationResult.Errors, e => e.PropertyName == "PassengersNumber" &&
                                                          e.ErrorMessage == ValidatorErrorMessages.PassengerNumber_EqualOrLowerToZero);
        }

        [Fact]
        public void Should_Not_Have_Error_When_VehicleType_And_PassengerNumber_Are_Valid()
        {
            var model = new InsertVehicleDetailRequestModel
            {
                VehicleType = VehicleTypeEnum.Car,
                PassengersNumber = 4
            };

            var validationResult = _validator.Validate(model);

            // Assert
            Assert.True(validationResult.IsValid);
            Assert.Empty(validationResult.Errors);
        }
    }
}
