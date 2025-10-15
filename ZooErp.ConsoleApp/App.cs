using System.Linq;
using ZooErp.Domain.Interfaces;
using ZooErp.Domain.Models;
using ZooErp.Domain.Services;

namespace ZooErp.ConsoleApp;

/// <summary>
/// Класс, отвечающий за логику работы консольного приложения и UI.
/// Принцип единственной ответственности (SRP).
/// </summary>
public class App
{
    private readonly Zoo _zoo;
    private int _nextInventoryNumber = 200;

    public App(Zoo zoo)
    {
        _zoo = zoo;
    }

    public void Run()
    {
        // Изначально животных нет (демо-добавления убраны)
        
        Console.WriteLine("\n--- Welcome to the Zoo ERP System! ---");

        bool running = true;
        while (running)
        {
            PrintMenu();
            string? choice = Console.ReadLine()?.Trim();
            switch (choice)
            {
                case "1":
                    AddNewAnimal();
                    break;
                case "2":
                    ShowTotalFoodReport();
                    break;
                case "3":
                    ShowContactZooReport();
                    break;
                case "4":
                    ShowInventoryReport();
                    break;
                case "5":
                    ShowAllAnimals();
                    break;
                case "6":
                    running = false;
                    break;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
            if (running)
            {
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
            }
        }
    }

    private void PrintMenu()
    {
        Console.Clear();
        Console.WriteLine("Zoo Management Menu:");
        Console.WriteLine("1. Add a new animal");
        Console.WriteLine("2. Show total food report");
        Console.WriteLine("3. Show contact zoo report");
        Console.WriteLine("4. Show inventory list");
        Console.WriteLine("5. Show all animals in the zoo");
        Console.WriteLine("6. Exit");
        Console.Write("Select an option: ");
    }

    private void AddNewAnimal()
    {
        Console.WriteLine("\nSelect animal type to add:");
        Console.WriteLine("1. Tiger");
        Console.WriteLine("2. Wolf");
        Console.WriteLine("3. Rabbit");
        Console.WriteLine("4. Monkey");
        Console.Write("Enter choice: ");

        var typeChoice = Console.ReadLine()?.Trim();
        if (!new[] { "1", "2", "3", "4" }.Contains(typeChoice))
        {
            Console.WriteLine("Invalid animal type.");
            return;
        }

        bool isHealthy = ReadYesNo("Is the animal healthy? (да/нет | yes/no): ");

        IAnimal newAnimal = typeChoice switch
        {
            "1" => new Tiger(_nextInventoryNumber, isHealthy),
            "2" => new Wolf(_nextInventoryNumber, isHealthy),
            "3" => new Rabbit(_nextInventoryNumber, isHealthy),
            "4" => new Monkey(_nextInventoryNumber, isHealthy),
            _ => throw new InvalidOperationException("Unexpected animal type.")
        };

        bool accepted = _zoo.AcceptAnimal(newAnimal);
        if (!accepted)
        {
            Console.WriteLine("Animal was not accepted (health check failed).");
        }
        else
        {
            Console.WriteLine($"Animal added: {newAnimal.Name} (Inventory No: {_nextInventoryNumber})");
            _nextInventoryNumber++;
        }
    }

    private static bool ReadYesNo(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            string? s = Console.ReadLine();
            if (s == null) return false;

            s = s.Trim().ToLowerInvariant();

            if (s is "да" or "д" or "yes" or "y" or "true" or "1" or "ok" or "sure")
                return true;
            if (s is "нет" or "н" or "no" or "n" or "false" or "0")
                return false;

            Console.WriteLine("Please enter yes/no (да/нет).");
        }
    }

    private void ShowAllAnimals()
    {
        Console.WriteLine("\n--- List of all animals in the Zoo ---");
        var animals = _zoo.GetAnimals();
        if (!animals.Any())
        {
            Console.WriteLine("The zoo is empty.");
            return;
        }

        foreach (var animal in animals)
        {
            Console.WriteLine($"- {animal.Name} (Inventory No: {animal.InventoryNumber}, Food/day: {animal.FoodPerDay}kg)");
        }
    }

    private void ShowTotalFoodReport()
    {
        int totalFood = _zoo.CalculateTotalFoodNeeded();
        Console.WriteLine($"\nTotal food required for all animals per day: {totalFood} kg.");
    }

    private void ShowContactZooReport()
    {
        Console.WriteLine("\n--- Animals suitable for Contact Zoo ---");
        var contactAnimals = _zoo.GetAnimalsForContactZoo();
        if (!contactAnimals.Any())
        {
            Console.WriteLine("No animals are suitable for the contact zoo at the moment.");
            return;
        }

        foreach (var animal in contactAnimals)
        {
            Console.WriteLine($"- {animal.Name} (Kindness: {animal.KindnessLevel}/10)");
        }
    }

    private void ShowInventoryReport()
    {
        Console.WriteLine("\n--- Full Inventory List ---");
        var inventoryItems = _zoo.GetInventoryList();
        foreach (var item in inventoryItems)
        {
            if (item is Thing thing)
            {
                Console.WriteLine($"[Thing] Name: {thing.Name}, Inventory No: {thing.InventoryNumber}");
            }
            else if (item is IAnimal animal)
            {
                Console.WriteLine($"[Animal] Name: {animal.Name}, Inventory No: {animal.InventoryNumber}");
            }
        }
    }
}