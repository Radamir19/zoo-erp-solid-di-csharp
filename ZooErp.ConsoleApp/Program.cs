using Microsoft.Extensions.DependencyInjection;
using ZooErp.ConsoleApp;

// Точка входа в приложение
var serviceProvider = DiContainerConfig.Configure();

// Получаем корневой объект приложения из DI-контейнера
var app = serviceProvider.GetService<App>();
app?.Run();