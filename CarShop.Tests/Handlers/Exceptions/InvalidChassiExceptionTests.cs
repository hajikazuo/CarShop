using Bogus;
using CarShop.Api.Handlers.Exceptions;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarShop.Tests.Handlers.Exceptions
{
    [Trait("Category", "InvalidChassiException")]
    public sealed class InvalidChassiExceptionTests
    {
        private readonly Faker _faker = new("pt_BR");

        [Fact]
        public void Constructor_GivenMessage_ThenSholdSetMessageToException() 
        {
            // Arrange
            var message = _faker.Lorem.Paragraph();

            // Act
            var exception = new InvalidChassiException(message);

            // Assert
            exception.Message.Should().Be(message);
        }
    }
}
