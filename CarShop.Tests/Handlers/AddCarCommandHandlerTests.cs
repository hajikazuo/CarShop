using Bogus;
using CarShop.Api.Context;
using CarShop.Api.Handlers;
using CarShop.Api.Handlers.Exceptions;
using CarShop.Api.Services;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NSubstitute;

namespace CarShop.Tests.Handlers
{
    [Trait("Category", "AddCarCommandHandlerTests")]
    public sealed class AddCarCommandHandlerTests
    {
        private readonly AddCarCommandHandler _handler;
        private readonly Faker _faker = new Faker("pt_BR");
        private readonly ICarChassiValidatorService _mockCarChassiService;
        private readonly AppDbContext _dbContext;

        public AddCarCommandHandlerTests()
        {
            // App Db Context
            var dbContextOptions = new DbContextOptionsBuilder<AppDbContext>();
            dbContextOptions.UseInMemoryDatabase("CarShopTests");
            _dbContext = new AppDbContext(dbContextOptions.Options); 

            // Moq Car Chassi
            _mockCarChassiService = Substitute.For<ICarChassiValidatorService>();
            _handler = new AddCarCommandHandler(_mockCarChassiService, _dbContext);
        }

        [Fact]
        public async Task Handle_GivenChassiInvalid_ThenShouldThrowException()
        {
            //Given invalid chassi command
            var carName = _faker.Vehicle.Model();
            var invalidCommand = new AddCarCommand(carName);

            // Given invalid chassi
            _mockCarChassiService.CheckIfValidAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(Task.FromResult(false)); 
            
            // When handle
            var resultAction = () => _handler.Handle(invalidCommand, CancellationToken.None);

            // Then should return exception
            await resultAction.Should().ThrowAsync<InvalidChassiException>()
                .WithMessage($"[{carName}] chassi invalido!");
        }

        [Fact]
        public async Task Handle_GivenChassiValid_ThenShouldInsertAndReturnNewCar()
        {
            // Given valid command
            var expectedCarName = _faker.Vehicle.Model();
            var validCommand = new AddCarCommand(expectedCarName);

            // Given valid chassi
            _mockCarChassiService.CheckIfValidAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(Task.FromResult(true));

            // When handle is called
            var result = await _handler.Handle(validCommand, CancellationToken.None);
            _dbContext.ChangeTracker.Clear();

            // Then should insert and return new car
            result.Name.Should().Be(expectedCarName);
            result.Id.Should().NotBeEmpty();

            var carId = result.Id;  
            var insertedCar = await _dbContext.Cars.SingleAsync(c => c.Id == carId, CancellationToken.None);

            insertedCar.Should().NotBeNull();
            insertedCar.Name.Should().Be(expectedCarName);
        }
    }
}
