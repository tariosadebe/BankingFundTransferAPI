using BankingFundTransferAPI.Data;
using BankingFundTransferAPI.Models;
using BankingFundTransferAPI.Repositories.Implementations;
using BankingFundTransferAPI.Repositories.Interfaces;
using BankingFundTransferAPI.Services.Implementations;
using BankingFundTransferAPI.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;

namespace BankingFundTransferAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            // Initialize the database
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<BankingDbContext>();
                    // Ensure the database is created and seed initial data if necessary
                    context.Database.EnsureCreated();
                    DatabaseInitializer.Initialize(context);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred creating the DB.");
                }
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureAppConfiguration((hostingContext, config) =>
                    {
                        var env = hostingContext.HostingEnvironment;
                        config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                              .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);
                        config.AddEnvironmentVariables();
                    });

                    webBuilder.ConfigureServices((context, services) =>
                    {
                        // Database configuration
                        services.AddDbContext<BankingDbContext>(options =>
                            options.UseSqlServer(context.Configuration.GetConnectionString("DefaultConnection")));

                        // AutoMapper configuration
                        services.AddAutoMapper(typeof(Program));

                        // Repositories
                        services.AddScoped<IAccountRepository, AccountRepository>();
                        services.AddScoped<ITransactionRepository, TransactionRepository>();

                        // Services
                        services.AddScoped<ITransactionService, TransactionService>();

                        // Controllers
                        services.AddControllers();

                        // Swagger configuration
                        services.AddSwaggerGen(c =>
                        {
                            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Banking API", Version = "v1" });
                        });
                    });

                    webBuilder.Configure((context, app) =>
                    {
                        var env = context.HostingEnvironment;

                        if (env.IsDevelopment())
                        {
                            app.UseDeveloperExceptionPage();
                            app.UseSwagger();
                            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Banking API v1"));
                        }

                        app.UseHttpsRedirection();

                        app.UseRouting();

                        app.UseAuthorization();

                        app.UseEndpoints(endpoints =>
                        {
                            endpoints.MapControllers();
                        });
                    });
                });
    }
}
