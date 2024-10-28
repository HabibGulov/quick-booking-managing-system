using Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddBookingRepositories(this IServiceCollection services)
    {
        services.AddTransient<IClientReposiory, ClientRepository>();
        services.AddTransient<IWorkerRepository, WorkerRepository>();
        services.AddTransient<IServiceRepository, ServiceRepository>();
        services.AddTransient<IAppointmentRepository, AppointmentRepository>();

        return services;
    }
}
