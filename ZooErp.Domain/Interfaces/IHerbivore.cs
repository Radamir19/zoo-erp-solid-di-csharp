namespace ZooErp.Domain.Interfaces;

/// <summary>
/// Определяет травоядное животное.
/// </summary>
public interface IHerbivore : IAnimal
{
    /// <summary>
    /// Уровень доброты по шкале от 1 до 10.
    /// </summary>
    int KindnessLevel { get; }
}