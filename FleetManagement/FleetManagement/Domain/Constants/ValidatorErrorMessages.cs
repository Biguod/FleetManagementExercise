namespace FleetManagement.Domain.Constants
{
    public class ValidatorErrorMessages
    {
        public static string ChassisId_IsNull = "ChassisId is required!";
        public static string ChassisId_IsEmpty = "ChassisId must have a value! Ex.: ABC123546";
        public static string ChassisId_InvalidFormat = "ChassisId invalid format! Ex.: ABC123546";

        public static string ChassisSeries_IsNull = "Chassis Series is required!";
        public static string ChassisSeries_IsEmpty = "Chassis Series must have a value! Ex.: ABCDE";
        public static string ChassisSeries_InvalidFormat = "Chassis Series invalid format! Ex.: ABC123546";

        public static string ChassisNumber_EqualOrLowerToZero = "Chassis Number is required and must be greater than zero (0)!";

        public static string Color_IsNull = "Color is required!";
        public static string Color_IsEmpty = "Color must have a value! Ex.: Red";

        public static string VehicleType_InvalidEnum = "Invalid Vehicle Type!";

        public static string PassengerNumber_EqualOrLowerToZero = "Passenger Number is required and must be greater than zero (0)!";

        public static string VehicleDetailId_IsNull = "Vehicle Detail Id is required!";
        public static string VehicleDetailId_IsEmpty = "Vehicle Detail Id must have a value!";
    }
}
