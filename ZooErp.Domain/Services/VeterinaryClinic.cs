using ZooErp.Domain.Interfaces;

namespace ZooErp.Domain.Services;

/// <summary>
/// Конкретная реализация ветеринарной клиники.
/// Принцип единственной ответственности (SRP): Класс отвечает только за проверку здоровья.
/// </summary>
public class VeterinaryClinic : IVeterinaryClinic
{
    public bool CheckHealth(IAnimal animal)
    {
        // В реальном приложении здесь была бы сложная логика.
        // Для примера, просто возвращаем свойство животного.
        Console.WriteLine($"Checking health for {animal.Name} (Inventory No: {animal.InventoryNumber})... Result: {(animal.IsHealthy ? "Healthy" : "Not Healthy")}");
        return animal.IsHealthy;
    }
}