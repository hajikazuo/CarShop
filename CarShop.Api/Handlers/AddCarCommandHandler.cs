using CarShop.Api.Context;
using CarShop.Api.Dtos;
using CarShop.Api.Services;
using MediatR;
using CarShop.Api.Entities;
using CarShop.Api.Handlers.Exceptions;

namespace CarShop.Api.Handlers
{
    internal class AddCarCommandHandler(ICarChassiValidatorService carChassiValidatorService, AppDbContext appDbContext)
    : IRequestHandler<AddCarCommand, CarDto>
    {
        public async Task<CarDto> Handle(AddCarCommand request, CancellationToken cancellationToken)
        {
            // Generating Id
            var id = Guid.NewGuid();

            var isValidChassi = await carChassiValidatorService.CheckIfValidAsync(id, cancellationToken);

            if (!isValidChassi)
                throw new InvalidChassiException($"[{request.Nome}] chassi invalido!");

            var car = new Car(id, request.Nome);

            await appDbContext.Cars.AddAsync(car, cancellationToken);
            await appDbContext.SaveChangesAsync(cancellationToken);

            return new CarDto(car.Id, car.Name);
        }
    }
}
