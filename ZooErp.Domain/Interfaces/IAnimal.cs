namespace ZooErp.Domain.Interfaces;

/// <summary>
/// Определяет животное. Комбинирует IAlive и IInventory.
/// </summary>
public interface IAnimal : IAlive, IInventory
{
    string Name { get; }
    bool IsHealthy { get; }
}