using Microsoft.Extensions.DependencyInjection;
using ZooErp.Domain.Interfaces;
using ZooErp.Domain.Services;

namespace ZooErp.ConsoleApp;

public static class DiContainerConfig
{
    public static ServiceProvider Configure()
    {
        var services = new ServiceCollection();

        // Регистрируем зависимости: интерфейс -> конкретный класс
        // Transient: новый экземпляр при каждом запросе
        // Scoped: новый экземпляр для каждого "scope" (актуально для web)
        // Singleton: один экземпляр на всё время жизни приложения
        services.AddSingleton<IVeterinaryClinic, VeterinaryClinic>();
        services.AddSingleton<Zoo>();
        services.AddSingleton<App>();

        return services.BuildServiceProvider();
    }
}