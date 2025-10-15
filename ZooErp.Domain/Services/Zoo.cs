using ZooErp.Domain.Interfaces;
using ZooErp.Domain.Models;

namespace ZooErp.Domain.Services;

/// <summary>
/// Основной класс, управляющий зоопарком.
/// Принцип инверсии зависимостей (DIP): Зависит от абстракции IVeterinaryClinic.
/// </summary>
public class Zoo
{
    private readonly IVeterinaryClinic _clinic;
    private readonly List<IAnimal> _animals = new();
    private readonly List<IInventory> _inventory = new();

    public Zoo(IVeterinaryClinic clinic)
    {
        _clinic = clinic;
        
        // Добавим несколько вещей на баланс для примера
        _inventory.Add(new Table(101));
        _inventory.Add(new Computer(102));
    }

    public bool AcceptAnimal(IAnimal newAnimal)
    {
        Console.WriteLine($"Attempting to accept a new {newAnimal.Name}...");
        if (_clinic.CheckHealth(newAnimal))
        {
            _animals.Add(newAnimal);
            _inventory.Add(newAnimal); // Животные также являются инвентарём
            Console.WriteLine($"{newAnimal.Name} has been accepted to the zoo.");
            return true;
        }

        Console.WriteLine($"The {newAnimal.Name} is not healthy and cannot be accepted.");
        return false;
    }

    public int CalculateTotalFoodNeeded()
    {
        return _animals.Sum(animal => animal.FoodPerDay);
    }

    public IEnumerable<IHerbivore> GetAnimalsForContactZoo()
    {
        // Используем LINQ для фильтрации травоядных с уровнем доброты > 5
        return _animals.OfType<IHerbivore>().Where(h => h.KindnessLevel > 5);
    }

    public IEnumerable<IInventory> GetInventoryList()
    {
        return _inventory.OrderBy(item => item.InventoryNumber);
    }
    
    public IEnumerable<IAnimal> GetAnimals() => _animals;
}