using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Application;

public static class ApplicationDependesInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(ApplicationDependesInjection).Assembly));

        // Регистрируем кэш
        services.RegisterCaching();

        return services;
    }

    private static void RegisterCaching(this IServiceCollection services)
    {
        // Регистрация кэша в памяти
        services.AddMemoryCache();
    }
}
