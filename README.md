# ERP-система для Московского зоопарка 🦁🐰

## 📋 Содержание
- [Структура проекта](#структура-проекта)
- [Применение принципов SOLID](#применение-принципов-solid)
- [Установка и запуск](#установка-и-запуск)
- [Использование](#использование)
- [Тестирование](#тестирование)
- [Архитектура классов](#архитектура-классов)
- [Автор](#автор)

## 🗂 Структура проекта

```
ZOO/
├── ZooErp.Domain/              # Доменная логика (Domain Layer)
│   ├── Interfaces/             # Интерфейсы
│   │   ├── IAlive.cs          # Живая сущность (потребляет еду)
│   │   ├── IAnimal.cs         # Животное
│   │   ├── IHerbivore.cs      # Травоядное
│   │   ├── IInventory.cs      # Инвентаризируемый объект
│   │   └── IVeterinaryClinic.cs # Ветклиника
│   ├── Models/                 # Модели данных
│   │   ├── Animal.cs          # Базовый класс Animal + Herbivore/Predator
│   │   └── Thing.cs           # Предметы (Table, Computer)
│   └── Services/               # Сервисы
│       ├── Zoo.cs             # Основная логика зоопарка
│       └── VeterinaryClinic.cs # Проверка здоровья животных
│
├── ZooErp.ConsoleApp/          # Консольное приложение (Presentation Layer)
│   ├── App.cs                 # UI и меню приложения
│   ├── Program.cs             # Точка входа
│   └── DiContainerConfig.cs   # Настройка DI-контейнера
│
├── ZooErp.Tests/               # Юнит-тесты
│   ├── ZooTests.cs            # Тесты логики Zoo
│   └── AnimalHierarchyTests.cs # Тесты иерархии классов
│
└── README.md                   # Этот файл
```

---

## 🧩 Применение принципов SOLID

### **S** — Single Responsibility Principle (Принцип единственной ответственности)
Каждый класс отвечает за одну вещь:
- `VeterinaryClinic` — только проверка здоровья животных
- `Zoo` — управление коллекциями и бизнес-логика
- `App` — консольный UI и взаимодействие с пользователем
- `DiContainerConfig` — настройка DI-контейнера

### **O** — Open/Closed Principle (Принцип открытости/закрытости)
Система открыта для расширения, закрыта для модификации:
- Добавление новых животных через наследование от `Herbivore` или `Predator`
- Не требуется изменять существующий код `Zoo` или `VeterinaryClinic`

```csharp
// Пример: добавляем нового травоядного
public class Deer(int inventoryNumber, bool isHealthy)
    : Herbivore("Deer", 8, inventoryNumber, isHealthy, 7)
{
}
```

### **L** — Liskov Substitution Principle (Принцип подстановки Барбары Лисков)
Любой объект типа `IAnimal`, `IHerbivore` взаимозаменяем в коллекциях:
```csharp
IAnimal animal = new Rabbit(100, true); // или Tiger, Wolf, Monkey
_zoo.AcceptAnimal(animal); // Работает для всех типов животных
```

### **I** — Interface Segregation Principle (Принцип разделения интерфейсов)
Небольшие, специализированные интерфейсы:
- `IAlive` — потребление еды
- `IInventory` — инвентарный номер
- `IAnimal` — комбинация IAlive + IInventory
- `IHerbivore` — добавляет уровень доброты
- `IVeterinaryClinic` — проверка здоровья

Клиенты зависят только от тех интерфейсов, которые им нужны.

### **D** — Dependency Inversion Principle (Принцип инверсии зависимостей)
Высокоуровневые модули не зависят от низкоуровневых:
- `Zoo` зависит от абстракции `IVeterinaryClinic`, а не от конкретной реализации
- Реализация `VeterinaryClinic` внедряется через DI-контейнер
- Легко подменить реализацию для тестирования (используем Moq)

```csharp
public class Zoo
{
    private readonly IVeterinaryClinic _clinic; // Зависимость от интерфейса
    
    public Zoo(IVeterinaryClinic clinic) // Внедрение через конструктор
    {
        _clinic = clinic;
    }
}
```

## 🚀 Установка и запуск

### Требования
- [.NET SDK 8.0+](https://dotnet.microsoft.com/download)
- Git (опционально)

### Клонирование репозитория
```bash
git clone https://github.com/Radamir19/zoo-erp-solid-di-csharp.git
cd zoo-erp-solid-di-csharp
```

### Восстановление зависимостей
```bash
dotnet restore
```

### Сборка проекта
```bash
dotnet build
```

### Запуск приложения
```bash
dotnet run --project ZooErp.ConsoleApp/ZooErp.ConsoleApp.csproj
```

---

## 💻 Использование

После запуска приложения вы увидите меню:

```
--- Welcome to the Zoo ERP System! ---
Zoo Management Menu:
1. Add a new animal
2. Show total food report
3. Show contact zoo report
4. Show inventory list
5. Show all animals in the zoo
6. Exit
Select an option:
```

## 🧪 Тестирование

### Запуск всех тестов
```bash
dotnet test
```

### Запуск с подробным выводом
```bash
dotnet test --verbosity normal
```

### Запуск с покрытием кода
```bash
dotnet test --collect:"XPlat Code Coverage"
```

### Что покрывают тесты
- ✅ Добавление здоровых и больных животных
- ✅ Подсчёт общей еды
- ✅ Фильтрация травоядных для контактного зоопарка
- ✅ Инвентаризация (животные + вещи)
- ✅ Работа VeterinaryClinic

### Результаты тестов
```
Пройдено: 15
Не пройдено: 0
Пропущено: 0
Покрытие: ~90%+
```

---

## 🏗 Архитектура классов

### Иерархия животных

```
IAnimal (interface)
    ↓
Animal (abstract)
    ↓
    ├── Herbivore (abstract) ← IHerbivore
    │   ├── Rabbit
    │   └── Monkey
    │
    └── Predator (abstract)
        ├── Tiger
        └── Wolf
```

### Диаграмма зависимостей

```
[App] ──(DI)──> [Zoo] ──(DI)──> [IVeterinaryClinic]
                  ↓                       ↑
            [IAnimal]              [VeterinaryClinic]
                  ↓
        ┌─────────┴─────────┐
   [Herbivore]         [Predator]
        ↓                    ↓
  Rabbit, Monkey      Tiger, Wolf
```

---

## 👨‍💻 Автор

**Радамир Нурмагомедов Ренатович**
- Проект: [zoo-erp-solid-di-csharp](https://github.com/Radamir19/zoo-erp-solid-di-csharp)
