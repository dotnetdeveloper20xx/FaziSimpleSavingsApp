using System;
using Application.Interfaces;
using FaziSimpleSavings.Application.RecurringDeposits.Queries;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(FaziSimpleSavings.AzureFunctions.Startup))]

namespace FaziSimpleSavings.AzureFunctions
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            // Connection string from local.settings.json or Azure App Settings
            var configuration = builder.GetContext().Configuration;
            var connectionString = configuration["SqlConnectionString"];

            // Inject DbContext
            builder.Services.AddDbContext<IAppDbContext, AppDbContext>(options =>
                options.UseSqlServer(connectionString));

            // Add MediatR handlers
            builder.Services.AddMediatR(typeof(GetDueRecurringDepositsQuery).Assembly);
        }
    }
}
