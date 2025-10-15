using ZooErp.Domain.Interfaces;

namespace ZooErp.Domain.Models;

/// <summary>
/// Базовый класс для неодушевленных предметов.
/// </summary>

public abstract class Thing(string name, int inventoryNumber) : IInventory
{
    public string Name { get; } = name;
    public int InventoryNumber { get; } = inventoryNumber;
}

public class Table(int inventoryNumber) : Thing("Table", inventoryNumber) { }
public class Computer(int inventoryNumber) : Thing("Computer", inventoryNumber) { }