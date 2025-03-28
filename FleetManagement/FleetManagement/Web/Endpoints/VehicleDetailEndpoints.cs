using System.Text.RegularExpressions;
using FleetManagement.Domain.Contracts.Request.VehicleDetail;
using FleetManagement.Domain.Interfaces.Services;
using FleetManagement.Domain.Models;
using FleetManagement.Web.Validators;

namespace FleetManagement.Web.Endpoints
{
    public static class VehicleDetailEndpoints
    {
        public static void AddVehicleDetailEndpoints(this IEndpointRouteBuilder routes)
        {
            routes.MapGet("/api/listvehicledetails", async (IVehicleDetailService service) =>
            {
                try
                {
                    var results = await service.GetAllVehicleDetails();
                    if (results == null)
                        return Results.NotFound("No Vehicle Details Found!");
                    return Results.Ok(results);
                }
                catch (Exception ex)
                {
                    return Results.Problem(detail: "Internal Error", statusCode: 500);
                }
            });

            routes.MapGet("/api/searchvehicledetailbyvehicletype", async (IVehicleDetailService service, VehicleTypeEnum vehicleType) =>
            {
                try
                {
                    var result = await service.GetVehicleDetailByVehicleType(vehicleType);
                    if (result == null)
                    {
                        return Results.NotFound("Vehicle Details does not exist!");
                    }
                    return Results.Ok(result);
                }
                catch (Exception ex)
                {
                    return Results.Problem(detail: "Internal Error", statusCode: 500);
                }
            });

            routes.MapPost("/api/insertvehicledetail", async (IVehicleDetailService service, InsertVehicleDetailRequestModel request) =>
            {
                try
                {
                    var validator = new InsertVehicleDetailRequestModelValidator().Validate(request);
                    if (validator.IsValid)
                    {
                        var result = await service.InsertVehicleDetail(request);
                        return Results.Ok(result);
                    }
                    return Results.BadRequest(validator.Errors.Select(s => s.ErrorMessage).ToList());
                }
                catch (Exception ex)
                {
                    return Results.Problem(detail: "Internal Error", statusCode: 500);
                }
            });

            routes.MapPut("/api/updatevehicledetail", async (IVehicleDetailService service, UpdateVehicleDetailRequestModel request) =>
            {
                try
                {
                    var validator = new UpdateVehicleDetailRequestModelValidator().Validate(request);
                    if (validator.IsValid)
                    {
                        var result = await service.UpdateVehicleDetail(request);
                        if (!result)
                        {
                            return Results.Problem("Couldn't update Vehicle Detail properly");
                        }
                        return Results.Ok(string.Format("Vehicle Detail {0} updated successfully!", request.Id));
                    }
                    return Results.BadRequest(validator.Errors.Select(s => s.ErrorMessage).ToList());
                }
                catch (Exception ex)
                {
                    return Results.Problem(detail: "Internal Error", statusCode: 500);
                }
            });

            routes.MapDelete("/api/deletevehicleDetail", async (IVehicleDetailService service, Guid vehicleDetailId) =>
            {
                try
                {
                    if (vehicleDetailId == null || vehicleDetailId.Equals(Guid.Empty)) return Results.BadRequest("Vehicle Detail Id invalid!");
                    var result = await service.DeleteVehicleDetail(vehicleDetailId);
                    if (!result)
                    {
                        return Results.Problem("Couldn't delete Vehicle Detail!");
                    }
                    return Results.Ok("Vehicle Detail Deleted!");
                }
                catch (Exception ex)
                {
                    return Results.Problem(detail: "Internal Error", statusCode: 500);
                }
            });
        }
    }
}
