using Bogus;
using CarShop.Api.Entities;
using FluentAssertions;

namespace CarShop.Tests.Entities
{
    [Trait("Category", "Car")]
    public sealed class CarTests
    {
        private readonly Faker _faker = new("pt_BR");

        [Fact]
        public void Constructor_GivenAllParameters_ThenShouldSetThePropertiesCorrectly()
        {
            //Arrange
            var expectedCarName = _faker.Vehicle.Model();    
            var expectedId = Guid.NewGuid();

            //Act
            var car = new Car(expectedId, expectedCarName);

            //Assert
            car.Id.Should().Be(expectedId);
            car.Name.Should().Be(expectedCarName);
        }
    }
}
