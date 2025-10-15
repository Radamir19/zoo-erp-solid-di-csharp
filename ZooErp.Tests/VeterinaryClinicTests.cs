using Xunit;
using ZooErp.Domain.Models;
using ZooErp.Domain.Services;

namespace ZooErp.Tests;

public class VeterinaryClinicTests
{
    [Fact]
    public void CheckHealth_ShouldReturnTrue_WhenAnimalIsHealthy()
    {
        // Arrange
        var clinic = new VeterinaryClinic();
        var healthyTiger = new Tiger(100, true);

        // Act
        bool result = clinic.CheckHealth(healthyTiger);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void CheckHealth_ShouldReturnFalse_WhenAnimalIsNotHealthy()
    {
        // Arrange
        var clinic = new VeterinaryClinic();
        var sickRabbit = new Rabbit(101, false);

        // Act
        bool result = clinic.CheckHealth(sickRabbit);

        // Assert
        Assert.False(result);
    }
}