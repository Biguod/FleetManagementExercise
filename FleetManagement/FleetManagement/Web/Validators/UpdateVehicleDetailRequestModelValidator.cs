using FleetManagement.Domain.Constants;
using FleetManagement.Domain.Contracts.Request.VehicleDetail;
using FluentValidation;

namespace FleetManagement.Web.Validators
{
    public class UpdateVehicleDetailRequestModelValidator : AbstractValidator<UpdateVehicleDetailRequestModel>
    {
        public UpdateVehicleDetailRequestModelValidator()
        {
            RuleFor(i => i.Id).NotNull().WithMessage(ValidatorErrorMessages.VehicleDetailId_IsNull)
                              .NotEmpty().WithMessage(ValidatorErrorMessages.VehicleDetailId_IsEmpty);

            RuleFor(i => i.VehicleType).IsInEnum().WithMessage(ValidatorErrorMessages.VehicleType_InvalidEnum);

            RuleFor(i => (int)i.PassengersNumber).GreaterThanOrEqualTo(1).WithMessage(ValidatorErrorMessages.PassengerNumber_EqualOrLowerToZero);
        }
    }
}
