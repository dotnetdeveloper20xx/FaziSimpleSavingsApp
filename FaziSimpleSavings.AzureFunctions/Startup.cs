
using Application.Interfaces;
using FaziSimpleSavings.Application.RecurringDeposits.Queries;
using Infrastructure.Persistence;
using MediatR;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices(services =>
    {
        // Connection string from environment or settings
        var config = new ConfigurationBuilder()
            .AddJsonFile("local.settings.json", optional: true)
            .AddEnvironmentVariables()
            .Build();

        var connectionString = config["SqlConnectionString"];

        // Register services
        services.AddDbContext<IAppDbContext, AppDbContext>(options =>
            options.UseSqlServer(connectionString));

        services.AddMediatR(typeof(GetDueRecurringDepositsQuery).Assembly);
    })
    .Build();

host.Run();
