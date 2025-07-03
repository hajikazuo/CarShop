using CarShop.Api.Services;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarShop.Tests.Services
{
    [Trait("Category", "CarChassiValidatorServiceTests")]
    public sealed class CarChassiValidatorServiceTests
    {
        [Fact]
        public async Task CheckIfValidAsync_GivenAnyParams_ThenShouldReturnTrueAsync()
        {
            // Given any params
            var anyId = Guid.NewGuid();
            var validator = new CarChassiValidatorService();

            var result = await validator.CheckIfValidAsync(anyId, CancellationToken.None);

            // Then should return true
            result.Should().BeTrue();

        }
    }
}
