using System.Text.RegularExpressions;
using FleetManagement.Domain.Constants;
using FleetManagement.Domain.Contracts.Request.Vehicle;
using FluentValidation;

namespace FleetManagement.Web.Validators
{
    public class InsertVehicleRequestModelValidator : AbstractValidator<InsertVehicleRequestModel>
    {
        public InsertVehicleRequestModelValidator() 
        {
            RuleFor(i => i.ChassisSeries).NotNull().WithMessage(ValidatorErrorMessages.ChassisSeries_IsNull)
                                         .NotEmpty().WithMessage(ValidatorErrorMessages.ChassisSeries_IsEmpty)
                                         .Matches(@"^[a-zA-Z0-9]+$").WithMessage(ValidatorErrorMessages.ChassisSeries_InvalidFormat);

            RuleFor(i => (int)i.ChassisNumber).GreaterThanOrEqualTo(1).WithMessage(ValidatorErrorMessages.ChassisNumber_EqualOrLowerToZero);

            RuleFor(i => i.Color).NotNull().WithMessage(ValidatorErrorMessages.Color_IsNull)
                                 .NotEmpty().WithMessage(ValidatorErrorMessages.Color_IsEmpty);
                
        }
    }
}
