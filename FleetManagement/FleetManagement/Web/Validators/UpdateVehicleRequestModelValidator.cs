using FleetManagement.Domain.Constants;
using FleetManagement.Domain.Contracts.Request.Vehicle;
using FluentValidation;

namespace FleetManagement.Web.Validators
{
    public class UpdateVehicleRequestModelValidator : AbstractValidator<UpdateVehicleRequestModel>
    {
        public UpdateVehicleRequestModelValidator()
        {
            RuleFor(i => i.ChassisId).NotNull().WithMessage(ValidatorErrorMessages.ChassisId_IsNull)
                                         .NotEmpty().WithMessage(ValidatorErrorMessages.ChassisId_IsEmpty)
                                         .Matches(@"^[a-zA-Z0-9]+$").WithMessage(ValidatorErrorMessages.ChassisSeries_InvalidFormat);


            RuleFor(i => i.Color).NotNull().WithMessage(ValidatorErrorMessages.Color_IsNull)
                                 .NotEmpty().WithMessage(ValidatorErrorMessages.Color_IsEmpty);
        }
    }
}
