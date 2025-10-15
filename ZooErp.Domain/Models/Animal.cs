using ZooErp.Domain.Interfaces;

namespace ZooErp.Domain.Models;

/// <summary>
/// Абстрактный базовый класс для всех животных.
/// Принцип открытости/закрытости (OCP): Можно добавлять новых животных, наследуясь от этого класса,
/// не изменяя существующий код, который работает с IAnimal.
/// </summary>

public abstract class Animal(string name, int foodPerDay, int inventoryNumber, bool isHealthy) : IAnimal
{
    public string Name { get; } = name;
    public int FoodPerDay { get; } = foodPerDay;
    public int InventoryNumber { get; } = inventoryNumber;
    public bool IsHealthy { get; } = isHealthy;
}

/// <summary>
/// Базовый класс для травоядных животных.
/// </summary>
public abstract class Herbo(string name, int foodPerDay ,int inventoryNumber, bool isHealthy, int KindnessLevel)
    : Animal(name, foodPerDay, inventoryNumber, isHealthy), IHerbivore
{
    public int KindnessLevel { get; } = KindnessLevel;

}

/// <summary>
/// Базовый класс для хищных животных.
/// </summary>
public abstract class Predator(string name, int foodPerDay, int inventoryNumber, bool isHealthy)
    : Animal(name, foodPerDay, inventoryNumber, isHealthy)
{
}

public class Rabbit(int inventoryNumber, bool isHealthy)
    : Herbo("Rabbit",2,inventoryNumber, isHealthy, 8)
{
}

public class Monkey(int inventoryNumber, bool isHealthy)
    : Herbo("Rabbit",2,inventoryNumber, isHealthy, 6)
{
}

public class Tiger(int inventoryNumber, bool isHealthy)
    : Predator("Tiger", 15, inventoryNumber, isHealthy)
{
}

public class Wolf(int inventoryNumber, bool isHealthy)
    : Predator("Wolf", 10, inventoryNumber, isHealthy)
{
}
