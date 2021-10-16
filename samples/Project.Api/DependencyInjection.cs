﻿using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Project.Api.Persistence;
using Project.Domain.Properties;
using Scenario;
using Scenario.Domain.SharedTypes;
using Scenario.Services;

namespace Project.Api
{
    public static class DependencyInjection
    {
        public static void AddScenarioProject(this IServiceCollection services)
        {
            services.AddTransient<IScenarioDataProvider, ProjectDataProvider>();
            services.AddScenario(typeof(AssemblyReference).Assembly);
        }

        public static IApplicationBuilder UseCurrentProject(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                EnsureDatabaseReady(scope.ServiceProvider);
            }

            return app;
        }

        private static void EnsureDatabaseReady(IServiceProvider serviceProvider)
        {
            var databaseContext = serviceProvider.GetRequiredService<DatabaseContext>();
            databaseContext.Database.Migrate();
        }
    }
}
