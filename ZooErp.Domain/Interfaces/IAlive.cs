namespace ZooErp.Domain.Interfaces;

/// <summary>
/// Определяет живую сущность, которая потребляет еду.
/// Принцип разделения интерфейсов (ISP): Маленький, сфокусированный интерфейс.
/// </summary>
public interface IAlive
{
    /// <summary>
    /// Количество еды, потребляемое в сутки (кг).
    /// </summary>
    int FoodPerDay { get; }
}