using CarShop.Api.Dtos;
using MediatR;

namespace CarShop.Api.Handlers
{
    internal record GetCarByIdQuery(Guid CardId) : IRequest<CarDto?>;
}
