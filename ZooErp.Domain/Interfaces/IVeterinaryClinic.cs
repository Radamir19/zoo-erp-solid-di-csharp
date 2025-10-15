namespace ZooErp.Domain.Interfaces;

/// <summary>
/// Абстракция для ветеринарной клиники.
/// Принцип инверсии зависимостей (DIP): Zoo зависит от этой абстракции, а не от конкретной реализации.
/// </summary>
public interface IVeterinaryClinic
{
    bool CheckHealth(IAnimal animal);
}