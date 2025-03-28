using System.Text.RegularExpressions;
using FleetManagement.Domain.Contracts.Request.Vehicle;
using FleetManagement.Domain.Interfaces.Services;
using FleetManagement.Web.Validators;

namespace FleetManagement.API.Endpoints
{
    public static class VehicleEndpoints
    {
        public static void AddVehicleEndpoints(this IEndpointRouteBuilder routes)
        {
            routes.MapGet("/api/listvehicles", async (IVehicleService service) =>
            {
                try
                {
                    var results = await service.GetAllVehicles();
                    if (results == null)
                        return Results.NotFound("No Vehicles Found!");
                    return Results.Ok(results);
                }
                catch (Exception ex)
                {
                    return Results.Problem(detail: "Internal Error", statusCode: 500);
                }
            });

            routes.MapGet("/api/searchvehiclebychassisid", async (IVehicleService service, string chassisId) =>
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(chassisId) || Regex.IsMatch(chassisId, "[^a-zA-Z0-9]+")) return Results.BadRequest("ChassisId invalid!");
                    var result = await service.GetVehicleByChassisId(chassisId);
                    if (result == null)
                    {
                        return Results.NotFound("Chassis Id does not exist!");
                    }
                    return Results.Ok(result);
                }
                catch (Exception ex)
                {
                    return Results.Problem(detail: "Internal Error", statusCode: 500);
                }
            });

            routes.MapPost("/api/insertvehicle", async (IVehicleService service, InsertVehicleRequestModel request) =>
            {
                try
                {
                    var validator = new InsertVehicleRequestModelValidator().Validate(request);
                    if (validator.IsValid)
                    {
                        var result = await service.InsertVehicle(request);
                        return Results.Ok(result);
                    }
                    return Results.BadRequest(validator.Errors.Select(s => s.ErrorMessage).ToList());
                }
                catch (Exception ex)
                {
                    return Results.Problem(detail: "Internal Error", statusCode: 500);
                }
            });

            routes.MapPut("/api/updatevehicle", async (IVehicleService service, UpdateVehicleRequestModel request) =>
            {
                try
                {
                    var validator = new UpdateVehicleRequestModelValidator().Validate(request);
                    if (validator.IsValid)
                    {
                        var result = await service.UpdateVehicle(request);
                        if (!result)
                        {
                            return Results.Problem("Couldn't update Vehicle properly");
                        }
                        return Results.Ok(string.Format("Vehicle {0} updated successfully!", request.ChassisId));
                    }
                    return Results.BadRequest(validator.Errors.Select(s => s.ErrorMessage).ToList());
                }
                catch (Exception ex)
                {
                    return Results.Problem(detail: "Internal Error", statusCode: 500);
                }
            });

            routes.MapDelete("/api/deletevehicle", async (IVehicleService service, string chassisId) =>
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(chassisId) || Regex.IsMatch(chassisId, "[^a-zA-Z0-9]+")) return Results.BadRequest("ChassisId invalid!");
                    var result = await service.DeleteVehicle(chassisId);
                    if (!result)
                    {
                        return Results.Problem("Couldn't delete Vehicle!");
                    }
                    return Results.Ok("Vehicle Deleted!");
                }
                catch (Exception ex)
                {
                    return Results.Problem(detail: "Internal Error", statusCode: 500);
                }
            });
        }
    }
}
