namespace TeamBuilder
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using TeamBuilder.Data;
    using TeamBuilder.Services.Contracts;
    using TeamBuilder.Services;
    using TeamBuilder.App.Core;

    class StartUp
    {
        static void Main()
        {
            var serviceProvider = ConfigureServices();

            var engine = new Engine(serviceProvider);
            engine.Run();
        }

        private static IServiceProvider ConfigureServices()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddDbContext<TeamBuilderDbContext>(options =>
            options.UseSqlServer(Configuration.ConnectionString));

            serviceCollection.AddTransient<IUserService, UserService>();

            serviceCollection.AddTransient<IEventService, EventService>();

            serviceCollection.AddTransient<IInvitationService, InvitationService>();

            serviceCollection.AddTransient<ITeamService, TeamService>();

            var serviceProvider = serviceCollection.BuildServiceProvider();

            return serviceProvider;
        }
    }
}
