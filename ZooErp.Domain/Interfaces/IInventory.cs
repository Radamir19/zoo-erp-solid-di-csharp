namespace ZooErp.Domain.Interfaces;

/// <summary>
/// Определяет сущность, подлежащую инвентаризации.
/// Принцип разделения интерфейсов (ISP).
/// </summary>
public interface IInventory
{
    /// <summary>
    /// Уникальный инвентарный номер.
    /// </summary>
    int InventoryNumber { get; }
}