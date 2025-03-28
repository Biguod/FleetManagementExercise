using FleetManagement.Domain.Constants;
using FleetManagement.Domain.Contracts.Request.Vehicle;
using FleetManagement.Domain.Contracts.Request.VehicleDetail;
using FleetManagement.Domain.Models;
using FluentValidation;

namespace FleetManagement.Web.Validators
{
    public class InsertVehicleDetailRequestModelValidator : AbstractValidator<InsertVehicleDetailRequestModel>
    {
        public InsertVehicleDetailRequestModelValidator()
        {
            RuleFor(i => i.VehicleType).IsInEnum().WithMessage(ValidatorErrorMessages.VehicleType_InvalidEnum);

            RuleFor(i => (int)i.PassengersNumber).GreaterThanOrEqualTo(1).WithMessage(ValidatorErrorMessages.PassengerNumber_EqualOrLowerToZero);
        }
    }    
}
