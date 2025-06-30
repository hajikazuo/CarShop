using CarShop.Api.Dtos;
using MediatR;

namespace CarShop.Api.Handlers
{
    internal record AddCarCommand(string Nome) : IRequest<CarDto>;
}
