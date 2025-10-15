using Moq;
using Xunit;
using ZooErp.Domain.Interfaces;
using ZooErp.Domain.Models;
using ZooErp.Domain.Services;

namespace ZooErp.Tests;

public class ZooTests
{
    private readonly Mock<IVeterinaryClinic> _mockClinic;
    private readonly Zoo _zoo;

    public ZooTests()
    {
        _mockClinic = new Mock<IVeterinaryClinic>();
        _zoo = new Zoo(_mockClinic.Object);
    }

    [Fact]
    public void AcceptAnimal_ShouldAddAnimal_WhenHealthy()
    {
        var tiger = new Tiger(301, true);
        _mockClinic.Setup(c => c.CheckHealth(tiger)).Returns(true);

        bool result = _zoo.AcceptAnimal(tiger);

        Assert.True(result);
        Assert.Contains(tiger, _zoo.GetAnimals());
        Assert.Contains(tiger, _zoo.GetInventoryList());
        _mockClinic.Verify(c => c.CheckHealth(tiger), Times.Once);
    }

    [Fact]
    public void AcceptAnimal_ShouldNotAddAnimal_WhenNotHealthy()
    {
        var wolf = new Wolf(302, false);
        _mockClinic.Setup(c => c.CheckHealth(wolf)).Returns(false);

        bool result = _zoo.AcceptAnimal(wolf);

        Assert.False(result);
        Assert.DoesNotContain(wolf, _zoo.GetAnimals());
    }

    [Fact]
    public void AcceptAnimal_ShouldAddMultipleAnimals()
    {
        var tiger = new Tiger(303, true);
        var rabbit = new Rabbit(304, true);
        var monkey = new Monkey(305, true);
        _mockClinic.Setup(c => c.CheckHealth(It.IsAny<IAnimal>())).Returns(true);

        _zoo.AcceptAnimal(tiger);
        _zoo.AcceptAnimal(rabbit);
        _zoo.AcceptAnimal(monkey);

        var animals = _zoo.GetAnimals().ToList();
        Assert.Equal(3, animals.Count);
        Assert.Contains(tiger, animals);
        Assert.Contains(rabbit, animals);
        Assert.Contains(monkey, animals);
    }

    [Fact]
    public void CalculateTotalFoodNeeded_ShouldReturnZero_WhenNoAnimals()
    {
        int totalFood = _zoo.CalculateTotalFoodNeeded();

        Assert.Equal(0, totalFood);
    }

    [Fact]
    public void CalculateTotalFoodNeeded_ShouldReturnCorrectSum()
    {
        // Arrange
        var tiger = new Tiger(303, true);
        var rabbit = new Rabbit(304, true);
        var monkey = new Monkey(305, true);
        _mockClinic.Setup(c => c.CheckHealth(It.IsAny<IAnimal>())).Returns(true);
        _zoo.AcceptAnimal(tiger);
        _zoo.AcceptAnimal(rabbit);
        _zoo.AcceptAnimal(monkey);

        // Act
        int totalFood = _zoo.CalculateTotalFoodNeeded();

        // Assert
        // Проверяем фактические значения из ваших классов
        int expectedTotal = tiger.FoodPerDay + rabbit.FoodPerDay + monkey.FoodPerDay;
        Assert.Equal(expectedTotal, totalFood);
        
        // Или, если хотите конкретное число (узнайте текущие значения):
        // Tiger = ?, Rabbit = ?, Monkey = ? → сумма = 19
        // Assert.Equal(19, totalFood);
    }

    [Fact]
    public void GetAnimalsForContactZoo_ShouldReturnEmpty_WhenNoAnimals()
    {
        var contactAnimals = _zoo.GetAnimalsForContactZoo();

        Assert.Empty(contactAnimals);
    }

    [Fact]
    public void GetAnimalsForContactZoo_ShouldReturnOnlyKindHerbivores()
    {
        var kindRabbit = new Rabbit(305, true);
        var kindMonkey = new Monkey(306, true);
        var tiger = new Tiger(307, true);
        var wolf = new Wolf(308, true);
        _mockClinic.Setup(c => c.CheckHealth(It.IsAny<IAnimal>())).Returns(true);

        _zoo.AcceptAnimal(kindRabbit);
        _zoo.AcceptAnimal(kindMonkey);
        _zoo.AcceptAnimal(tiger);
        _zoo.AcceptAnimal(wolf);

        var contactAnimals = _zoo.GetAnimalsForContactZoo().ToList();

        Assert.Equal(2, contactAnimals.Count);
        Assert.Contains(kindRabbit, contactAnimals);
        Assert.Contains(kindMonkey, contactAnimals);
    }

    [Fact]
    public void GetInventoryList_ShouldContainInitialThings()
    {
        var inventory = _zoo.GetInventoryList().ToList();

        Assert.True(inventory.Count >= 2);
        Assert.Contains(inventory, item => item is Thing && item.InventoryNumber == 101);
        Assert.Contains(inventory, item => item is Thing && item.InventoryNumber == 102);
    }

    [Fact]
    public void GetInventoryList_ShouldContainAnimalsAndThings()
    {
        var wolf = new Wolf(308, true);
        _mockClinic.Setup(c => c.CheckHealth(wolf)).Returns(true);
        _zoo.AcceptAnimal(wolf);

        var inventory = _zoo.GetInventoryList().ToList();

        Assert.Contains(wolf, inventory);
        Assert.True(inventory.Count >= 3);
    }

    [Fact]
    public void GetInventoryList_ShouldBeSortedByInventoryNumber()
    {
        var tiger = new Tiger(500, true);
        var rabbit = new Rabbit(200, true);
        var monkey = new Monkey(300, true);
        _mockClinic.Setup(c => c.CheckHealth(It.IsAny<IAnimal>())).Returns(true);

        _zoo.AcceptAnimal(tiger);
        _zoo.AcceptAnimal(rabbit);
        _zoo.AcceptAnimal(monkey);

        var inventory = _zoo.GetInventoryList().ToList();

        for (int i = 0; i < inventory.Count - 1; i++)
        {
            Assert.True(inventory[i].InventoryNumber <= inventory[i + 1].InventoryNumber);
        }
    }

    [Fact]
    public void GetAnimals_ShouldReturnEmptyList_WhenNoAnimalsAccepted()
    {
        var animals = _zoo.GetAnimals();

        Assert.Empty(animals);
    }

    [Fact]
    public void Rabbit_ShouldHaveCorrectProperties()
    {
        var rabbit = new Rabbit(200, true);

        Assert.Equal("Rabbit", rabbit.Name);
        Assert.Equal(200, rabbit.InventoryNumber);
        Assert.True(rabbit.IsHealthy);
        Assert.Equal(8, rabbit.KindnessLevel);
    }

    [Fact]
    public void Tiger_ShouldBePredator()
    {
        var tiger = new Tiger(102, true);

        Assert.IsAssignableFrom<Predator>(tiger);
        Assert.IsAssignableFrom<IAnimal>(tiger);
    }
}