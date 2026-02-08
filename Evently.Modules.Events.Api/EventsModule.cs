using Evently.Modules.Events.Api.Database;
using Evently.Modules.Events.Api.Events;

using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Evently.Modules.Events.Api;

/// <summary>
/// Acts as the composition rule for this module
/// Is responsible for DI, registering endpoints with the web API,
/// anything related to cross-cutting concerns
/// </summary>
public static class EventsModule
{
    /// <summary>
    /// Registers endpoints with the web API
    /// </summary>
    /// <param name="app"></param>
    public static void MapEndpoints(IEndpointRouteBuilder app)
    {
        CreateEvent.MapEndpoint(app);
        GetEvent.MapEndpoint(app);
    }

    /// <summary>
    /// Configures the Events module by setting up dependency injection and database integration.
    /// </summary>
    /// <param name="services">The service collection to which services are added.</param>
    /// <param name="configuration">The application configuration that provides access to configuration settings.</param>
    /// <returns>The modified service collection.</returns> 
    public static IServiceCollection AddEventsModule(this IServiceCollection services, IConfiguration configuration)
    {
        // configure DI here
        string databaseConnectionString = configuration.GetConnectionString("Database")!;

        _ = services.AddDbContext<EventsDbContext>(options =>
            options
                .UseNpgsql(databaseConnectionString,
                    npgsqlOptions =>
                        npgsqlOptions.MigrationsHistoryTable(HistoryRepository.DefaultTableName, Schemas.Events))
                .UseSnakeCaseNamingConvention());

        return services;
    }
}