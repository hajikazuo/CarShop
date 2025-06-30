using CarShop.Api.Handlers.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CarShop.Api.Handlers;

namespace CarShop.Api.Controllers
{
    public static class CarsController
    {
        public static void MapCarsController(this WebApplication app)
        {
            var carsGroup = app.MapGroup("cars");

            // GET BY ID
            carsGroup.MapGet(":id", async (Guid id, ISender sender, CancellationToken ct = default) =>
            {
                var query = new GetCarByIdQuery(id);
                var result = await sender.Send(query, ct);

                return result is null ? Results.NotFound() : Results.Ok(result);
            });

            // ADD CARS
            carsGroup.MapPost("", async (AddCarRequest req, ISender sender, CancellationToken ct = default) =>
            {
                var command = new AddCarCommand(req.Name);

                try
                {
                    var result = await sender.Send(command, ct);
                    return Results.Ok(result);
                }
                catch (InvalidChassiException e)
                {
                    return Results.Conflict(new { error = e.Message });
                }

            });
        }
    }
}

public record AddCarRequest(string Name);